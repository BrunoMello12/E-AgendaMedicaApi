using eAgenda.Aplicacao.ModuloAutenticacao;
using eAgenda.Dominio.ModuloAutenticacao;
using eAgenda.Infra.Orm.Compartilhado;
using Microsoft.AspNetCore.Identity;

namespace E_AgendaMedicaApi.Config.IdentityConfig
{
    public static class IdentityConfigExtension
    {
        public static void ConfigurarIdentity(this IServiceCollection services)
        {
            services.AddTransient<ServicoAutenticacao>();

            services.AddIdentity<Usuario, IdentityRole<Guid>>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<eAgendaDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<eAgendaErrorDescriber>();
        }
    }
}
