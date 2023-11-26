using eAgenda.Dominio.Compartilhado;
using Microsoft.Win32;

namespace eAgenda.Dominio.ModuloConsulta
{
    public interface IRepositorioConsulta : IRepositorio<Consulta>
    {
        public Task<bool> ExisteConsultaNesseHorarioPorMedicoId(Guid medicoId, TimeSpan horaInicio, TimeSpan horaTermino, DateTime data);

        public Task<List<Consulta>> SelecionarConsultasMedico(Guid id);
    }
}
