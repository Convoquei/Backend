using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Convoquei.IoC.Configuracoes
{
    internal static class DependencyInjectionConfiguration
    {
        internal static void AddInjecoesDependencias(this IServiceCollection services)
        {
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName!.StartsWith("Convoquei."));

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses()
                .AsMatchingInterface()
                .WithScopedLifetime()
            );
        }
    }
}
