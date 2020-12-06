using Microsoft.Extensions.DependencyInjection;

namespace Hepsiburada.PlanetExploration.Domain
{
    public static class DomainServiceExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services.AddSingleton<PlateauManager, PlateauManager>();
        }
    }
}
