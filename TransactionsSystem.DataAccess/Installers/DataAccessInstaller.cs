using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionsSystem.Core.Installers;
using TransactionsSystem.DataAccess.InitialDataCreate;

namespace TransactionsSystem.DataAccess.Installers
{
    public class DataAccessInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ApplicationDbContext.ConnectionStringKey),
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly("TransactionsSystem.DataAccess")));
            services.AddHostedService<MigrationsService>();
        }
    }
}
