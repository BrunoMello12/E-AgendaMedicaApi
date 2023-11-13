using eAgenda.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;

namespace eAgenda.Infra.Orm.Compartilhado
{
    public class eAgendaDbContext : DbContext, IContextoPersistencia
    {
        public eAgendaDbContext(DbContextOptions options) : base(options)
        {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
