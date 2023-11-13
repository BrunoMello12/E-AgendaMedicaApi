using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Infra.Orm.Compartilhado;

namespace eAgenda.Infra.Orm.ModuloCirurgia
{
    public class RepositorioCirurgiaOrm : RepositorioBase<Cirurgia>
    {
        public RepositorioCirurgiaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }
    }
}
