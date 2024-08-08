using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Dominio.Compartilhado
{
    public interface ITenantProvider
    {
        public Guid Usuario_id { get; }
    }
}
