using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloMedico;

namespace eAgenda.Dominio.ModuloCirurgia
{
    public interface IRepositorioCirurgia : IRepositorio<Cirurgia>
    {
        public Task<List<Cirurgia>> SelecionarCirurgiasMedico(Guid id);
    }

}
