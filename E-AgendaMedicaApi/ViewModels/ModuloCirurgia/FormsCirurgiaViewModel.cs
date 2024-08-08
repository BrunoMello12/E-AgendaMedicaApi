using E_AgendaMedicaApi.ViewModels.ModuloMedico;

namespace E_AgendaMedicaApi.ViewModels.ModuloCirurgia
{
    public class FormsCirurgiaViewModel
    {
        public string Titulo { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public List<Guid> MedicosSelecionados { get; set; }
        public Guid UsuarioId { get; set; }

        public FormsCirurgiaViewModel()
        {
            MedicosSelecionados = new List<Guid>();
        }
    }
}
