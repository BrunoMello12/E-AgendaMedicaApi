using AutoMapper;
using E_AgendaMedicaApi.Config.TokenConfig;
using E_AgendaMedicaApi.ViewModels.ModuloAutenticacao;
using eAgenda.Aplicacao.ModuloAutenticacao;
using eAgenda.Dominio.ModuloAutenticacao;
using Microsoft.AspNetCore.Mvc;

namespace E_AgendaMedicaApi.Controllers
{
    [Route("api/autenticar")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ServicoAutenticacao service;
        private readonly IMapper map;

        public AutenticacaoController(ServicoAutenticacao service, IMapper map)
        {

            this.service = service;
            this.map = map;
        }

        [HttpPost("registrar")]
        [ProducesResponseType(typeof(TokenViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Registrar(RegistrarUsuarioViewModel usuarioVM)
        {
            var usuario = this.map.Map<Usuario>(usuarioVM);

            var usuarioResult = await service.RegistrarAsync(usuario, usuarioVM.Senha);

            if (usuarioResult.IsFailed)
            {
                return BadRequest(new
                {
                    Sucesso = false,
                    Errors = usuarioResult.Errors.Select(result => result.Message)
                });
            }

            var token = usuario.GerarJwt(DateTime.Now.AddDays(5));

            return Ok(new
            {
                Sucesso = true,
                Dados = token
            });
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Login(AutenticarUsuarioViewModel usuarioVM)
        {
            var usuarioResult = await service.LoginAsync(usuarioVM.Login, usuarioVM.Senha);

            if (usuarioResult.IsFailed)
            {
                return BadRequest(new
                {
                    Sucesso = false,
                    Errors = usuarioResult.Errors.Select(result => result.Message)
                });
            }

            var usuario = usuarioResult.Value;

            var token = usuario.GerarJwt(DateTime.Now.AddDays(5));

            return Ok(new
            {
                Sucesso = true,
                Dados = token
            });
        }

        [HttpPost("logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Logout()
        {
            await service.LogoutAsync();

            return Ok();
        }
    }
}
