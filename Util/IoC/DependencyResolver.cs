using Dominio.Interfaces.Repositorios.Usuario;
using Dominio.Interfaces.Servicos.Usuario;
using Dominio.Servicos.Usuario;
using Infra.Persistencia.AutoMapper;
using Infra.Persistencia.Repositorios.Usuario;
using Microsoft.Extensions.DependencyInjection;

namespace Util.IoC
{
    public class DependencyResolver
    {
        public static void Resolve(IServiceCollection services)
        {
            ResolveConfig(services);
            ResolveServicos(services);
            ResolveRepositorios(services);
        }

        private static void ResolveConfig(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton(new AutoMapperConfig().Config().CreateMapper());
        }

        private static void ResolveServicos(IServiceCollection services)
        {
            services.AddTransient<IServicoUsuario, ServicoUsuario>();  
        }

        private static void ResolveRepositorios(IServiceCollection services)
        {
            services.AddTransient<IRepositorioUsuario, RepositorioUsuario>(); 
        }
    }
}
