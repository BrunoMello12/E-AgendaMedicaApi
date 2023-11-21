using eAgenda.Dominio.ModuloCirurgia;

namespace E_AgendaMedicaApi.ViewModels.ModuloMedico
{
    public class SelecaoMedicoViewModel
    {
        public Guid Id { get; set; }
        public StatusMedicoCirurgia Status { get; set; }
    }
}
