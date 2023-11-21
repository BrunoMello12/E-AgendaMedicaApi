using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.ModuloMedico
{
    public interface IRepositorioMedico : IRepositorio<Medico>
    {
        List<Medico> SelecionarMuitos(List<Guid> idsCategoriasSelecionadas);

        List<Guid> SelecionarMuitos(List<Medico> medicos);
    }
}
