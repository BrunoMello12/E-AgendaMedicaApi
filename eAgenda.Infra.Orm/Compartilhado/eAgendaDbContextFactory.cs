using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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

            builder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=eAgendaMedicaOrm;Integrated Security=True");

            return new eAgendaDbContext(builder.Options);
        }
    }
}
