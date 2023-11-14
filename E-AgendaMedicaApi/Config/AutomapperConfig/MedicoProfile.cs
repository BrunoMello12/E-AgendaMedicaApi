using AutoMapper;
using E_AgendaMedicaApi.ViewModels.ModuloMedico;
using eAgenda.Dominio.ModuloMedico;

namespace E_AgendaMedicaApi.Config.AutomapperConfig
{
    public class MedicoProfile : Profile
    {
        public MedicoProfile()
        {
            CreateMap<Medico, ListarMedicoViewModel>();
            CreateMap<Medico, VisualizarMedicoViewModel>();
            CreateMap<FormsMedicoViewModel, Medico>();
        }
    }
}
