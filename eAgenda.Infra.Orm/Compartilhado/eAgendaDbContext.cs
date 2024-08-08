using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloAutenticacao;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloConsulta;
using eAgenda.Dominio.ModuloMedico;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;

namespace eAgenda.Infra.Orm.Compartilhado
{
    public class eAgendaDbContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>, IContextoPersistencia
    {
        private Guid usuario_id;

        public eAgendaDbContext(DbContextOptions options, ITenantProvider tenantProvider = null) : base(options)
        {
            if (tenantProvider != null)
            {
                this.usuario_id = tenantProvider.Usuario_id;
            }
        }

        public async Task<bool> GravarDadosAsync()
        {
            int registrosAfetados = await SaveChangesAsync();

            return registrosAfetados > 0;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            ILoggerFactory loggerFactory = LoggerFactory.Create((x) =>
            {
                x.AddSerilog(Log.Logger);
            });

            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Type tipo = typeof(eAgendaDbContext);

            Assembly dllComConfiguracoesOrm = tipo.Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(dllComConfiguracoesOrm);

            modelBuilder.Entity<Medico>().HasQueryFilter(x => x.UsuarioId == usuario_id);
            modelBuilder.Entity<Cirurgia>().HasQueryFilter(x => x.UsuarioId == usuario_id);
            modelBuilder.Entity<Consulta>().HasQueryFilter(x => x.UsuarioId == usuario_id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
