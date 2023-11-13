using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloConsulta;
using eAgenda.Infra.Orm.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infra.Orm.ModuloConsulta
{
    public class RepositorioConsultaOrm : RepositorioBase<Consulta>
    {
        public RepositorioConsultaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }
    }
}
