using eAgenda.Dominio.ModuloMedico;

namespace E_AgendaMedicaApi.ViewModels.ModuloCirurgia
{
    public class ListarCirurgiaViewModel
    {
        public string Titulo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
    }
}
