using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloMedico;
using eAgenda.Infra.Orm.Compartilhado;

namespace eAgenda.Infra.Orm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBase<Medico>
    {
        public RepositorioMedicoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }
    }
}
