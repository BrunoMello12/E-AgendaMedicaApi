using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloMedico;
using eAgenda.Infra.Orm.Compartilhado;

namespace eAgenda.Infra.Orm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBase<Medico>, IRepositorioMedico
    {
        public RepositorioMedicoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }


        public List<Medico> SelecionarMuitos(List<Guid> idsMedicosSelecionados)
        {
            return registros.Where(medico => idsMedicosSelecionados.Contains(medico.Id)).ToList();
        }

        public List<Guid> SelecionarMuitos(List<Medico> medicos)
        {
            return medicos.Select(medico => medico.Id).ToList();
        }
    }
}
