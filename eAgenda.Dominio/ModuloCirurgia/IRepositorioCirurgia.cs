using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloMedico;

namespace eAgenda.Dominio.ModuloCirurgia
{
    public interface IRepositorioCirurgia : IRepositorio<Cirurgia>
    {
        public Task<bool> ExisteCirurgiasNesseHorarioPorMedicoId(Guid medicoId, TimeSpan horaInicio, TimeSpan horaTermino, DateTime data, Guid? cirurgiaIdIgnorar);

        Task<List<Cirurgia>> SelecionarCirurgiasPorMedicoIds(List<Guid> medicosIds);

        public Task<List<Cirurgia>> SelecionarCirurgiasMedico(Guid id);
    }

}
