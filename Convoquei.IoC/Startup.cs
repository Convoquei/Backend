using Convoquei.IoC.Configuracoes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Convoquei.IoC
{
    public static class Startup
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDatabases(configuration);
            services.AddInjecoesDependencias();

            return services;
        }
    }
}
