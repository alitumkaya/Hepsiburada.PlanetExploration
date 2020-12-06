using Hepsiburada.PlanetExploration.ApplicationContract;
using Hepsiburada.PlanetExploration.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Hepsiburada.PlanetExploration.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddDomainServices()
                .AddSingleton<IPlanetService, PlanetService>();
        }
    }
}
