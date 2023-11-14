using AutoMapper;
using E_AgendaMedicaApi.ViewModels.ModuloCirurgia;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloMedico;

namespace E_AgendaMedicaApi.Config.AutomapperConfig
{
    public class CirurgiaProfile : Profile
    {
        public CirurgiaProfile()
        {
            CreateMap<Cirurgia, ListarCirurgiaViewModel>();

            CreateMap<Cirurgia, VisualizarCirurgiaViewModel>()
                .ForMember(destino => destino.Medicos, opt => opt.Ignore());

            CreateMap<FormsCirurgiaViewModel, Cirurgia>()
                .ForMember(destino => destino.Medicos, opt => opt.Ignore())
                .AfterMap<InserirMedicosMappingAction>();

            CreateMap<FormsCirurgiaViewModel, Cirurgia>()
                .ForMember(destino => destino.Medicos, opt => opt.Ignore())
                .AfterMap(EditarMedicosMappingAction);

        }

        private void EditarMedicosMappingAction(FormsCirurgiaViewModel viewModel, Cirurgia cirurgia)
        {
            viewModel.Medicos = cirurgia.Medicos.Select(medico => medico.Id).ToList();
        }
    }

    public class InserirMedicosMappingAction : IMappingAction<FormsCirurgiaViewModel, Cirurgia>
    {
        private readonly IRepositorioMedico repositorioMedico;

        public InserirMedicosMappingAction(IRepositorioMedico repositorioMedico)
        {
            this.repositorioMedico = repositorioMedico;
        }

        public void Process(FormsCirurgiaViewModel source, Cirurgia destination, ResolutionContext context)
        {
            destination.Medicos = repositorioMedico.SelecionarMuitos(source.Medicos);
        }
    }
}
