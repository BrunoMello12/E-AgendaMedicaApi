using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infra.Orm.Compartilhado
{
    internal class eAgendaDbContextFactory : IDesignTimeDbContextFactory<eAgendaDbContext>
    {
        public eAgendaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory
                .GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("PostgreSql");

            builder.UseNpgsql(connectionString);

            return new eAgendaDbContext(builder.Options);
        }
    }
}
