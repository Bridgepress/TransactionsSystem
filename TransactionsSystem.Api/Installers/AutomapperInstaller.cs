using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionsSystem.Core.Installers;

namespace TransactionsSystem.Api.Installers
{
    public class AutomapperInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ApiAssemblyMarker));
        }
    }
}
