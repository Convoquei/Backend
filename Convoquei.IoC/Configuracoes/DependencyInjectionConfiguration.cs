using Convoquei.Application.Organizacoes.Servicos;
using Convoquei.Core.Genericos.UoW;
using Convoquei.Core.Usuarios.Servicos;
using Convoquei.Infra.Genericos.UoW;
using Convoquei.Infra.Usuarios.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace Convoquei.IoC.Configuracoes
{
    internal static class DependencyInjectionConfiguration
    {
        internal static void AddInjecoesDependencias(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Aplicação
            services.Scan(scan => scan
                .FromAssemblyOf<OrganizacoesAppServico>()
                .AddClasses()
                .AsMatchingInterface()
                .WithScopedLifetime()
            );

            //Infra
            services.Scan(scan => scan
                .FromAssemblyOf<UsuariosRepositorio>()
                .AddClasses()
                .AsMatchingInterface()
                .WithScopedLifetime()
            );

            //Dominio
            services.Scan(scan => scan
                .FromAssemblyOf<CriarUsuarioServico>()
                .AddClasses()
                .AsMatchingInterface()
                .WithScopedLifetime()
            );
        }
    }
}
