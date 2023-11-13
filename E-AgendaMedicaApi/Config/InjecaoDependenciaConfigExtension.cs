using eAgenda.Dominio.Compartilhado;
using eAgenda.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace E_AgendaMedicaApi.Config
{
    public static class InjecaoDependenciaConfigExtension
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContext<IContextoPersistencia, eAgendaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            
        }
    }
}
