using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionsSystem.Core.Installers;
using TransactionsSystem.Domain;

namespace TransactionsSystem.Handlers.Installers
{
    public class HandlersInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(serviceConfiguration =>
                serviceConfiguration.RegisterServicesFromAssemblyContaining<HandlersInstaller>());
        }
    }
}
