using AutoMapper;
using E_AgendaMedicaApi.ViewModels.ModuloAutenticacao;
using eAgenda.Dominio.ModuloAutenticacao;

namespace E_AgendaMedicaApi.Config.AutomapperConfig
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<RegistrarUsuarioViewModel, Usuario>()
                .ForMember(usuario => usuario.UserName, opt => opt.MapFrom(usuarioVM => usuarioVM.Login));
            //.ForMember(usuario => usuario.PasswordHash, opt => opt.MapFrom(usuarioVM => usuarioVM.Senha))
        }
    }
}
