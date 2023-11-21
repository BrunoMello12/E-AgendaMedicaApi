using eAgenda.Dominio.ModuloCirurgia;

namespace E_AgendaMedicaApi.ViewModels.ModuloMedico
{
    public class VisualizarMedicoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Disponivel { get; set; }
        public string CRM { get; private set; }
        
    }
}
