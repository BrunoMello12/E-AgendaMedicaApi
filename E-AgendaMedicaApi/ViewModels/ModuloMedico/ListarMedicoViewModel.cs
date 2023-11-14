using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloConsulta;

namespace E_AgendaMedicaApi.ViewModels.ModuloMedico
{
    public class ListarMedicoViewModel
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Disponivel { get; set; }
    }
}
