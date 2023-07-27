using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsSystem.Core.Installers;
using TransactionsSystem.DataAccess;
using TransactionsSystem.Domain.Entities;
using TransactionsSystem.Repositories.Contracts;
using TransactionsSystem.Repositories.Implementantion.Repositories;

namespace TransactionsSystem.Repositories.Implementantion.Installers
{
    public class RepositoryInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();

            var repositories = typeof(RepositoryManager).Assembly.GetTypes()
                .Where(type => type.BaseType is not null && type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(RepositoryBase<>));

            foreach (var repository in repositories)
            {
                var repositoryInterface = repository.GetInterfaces()
                    .Single(i => !i.IsGenericType);
                services.AddScoped(repositoryInterface, repository);
            }
        }
    }
}
