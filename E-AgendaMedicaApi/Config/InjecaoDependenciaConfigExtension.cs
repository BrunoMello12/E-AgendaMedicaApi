using E_AgendaMedicaApi.Config.AutomapperConfig;
using eAgenda.Aplicacao.ModuloCirurgia;
using eAgenda.Aplicacao.ModuloConsulta;
using eAgenda.Aplicacao.ModuloMedico;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloConsulta;
using eAgenda.Dominio.ModuloMedico;
using eAgenda.Infra.Orm.Compartilhado;
using eAgenda.Infra.Orm.ModuloCirurgia;
using eAgenda.Infra.Orm.ModuloConsulta;
using eAgenda.Infra.Orm.ModuloMedico;
using Microsoft.EntityFrameworkCore;

namespace E_AgendaMedicaApi.Config
{
    public static class InjecaoDependenciaConfigExtension
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSql");

            services.AddDbContext<IContextoPersistencia, eAgendaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(connectionString);
            });

            services.AddTransient<IRepositorioCirurgia, RepositorioCirurgiaOrm>();
            services.AddTransient<ServicoCirurgia>();

            services.AddScoped<IRepositorioConsulta, RepositorioConsultaOrm>();
            services.AddTransient<ServicoConsulta>();

            services.AddScoped<IRepositorioMedico, RepositorioMedicoOrm>();
            services.AddTransient<ServicoMedico>();

            services.AddTransient<FormsCirurgiaMappingAction>();
            services.AddTransient<FormsCirurgiaMappingActionInverso>();

            services.AddTransient<ITenantProvider, ApiTenantProvider>();
            services.AddTransient<ManipuladorExcecoes>();
        }
    }
}
