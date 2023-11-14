using AutoMapper;
using E_AgendaMedicaApi.Controllers.Shared;
using E_AgendaMedicaApi.ViewModels.ModuloConsulta;
using E_AgendaMedicaApi.ViewModels.ModuloMedico;
using eAgenda.Aplicacao.ModuloMedico;
using eAgenda.Dominio.ModuloMedico;
using Microsoft.AspNetCore.Mvc;

namespace E_AgendaMedicaApi.Controllers
{
    [Route("api/medicos")]
    [ApiController]
    public class MedicoController : ApiControllerBase
    {
        private readonly ServicoMedico servicoMedico;
        private readonly IMapper mapeador;

        public MedicoController(ServicoMedico servicoMedico, IMapper mapeadorConsulta)
        {
            this.servicoMedico = servicoMedico;
            this.mapeador = mapeadorConsulta;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var medicoResult = await servicoMedico.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarMedicoViewModel>>(medicoResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var medicoResult = await servicoMedico.SelecionarPorIdAsync(id);

            if (medicoResult.IsFailed)
                return NotFound(medicoResult.Errors);

            var viewModel = mapeador.Map<VisualizarMedicoViewModel>(medicoResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsMedicoViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(FormsMedicoViewModel medicoViewModel)
        {
            var medico = mapeador.Map<Medico>(medicoViewModel);

            var medicoResult = await servicoMedico.InserirAsync(medico);

            return ProcessarResultado(medicoResult.ToResult(), medicoViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, FormsMedicoViewModel medicoViewModel)
        {
            var resultadoSelecao = await servicoMedico.SelecionarPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var medico = mapeador.Map(medicoViewModel, resultadoSelecao.Value);

            var medicoResult = await servicoMedico.EditarAsync(medico);

            return ProcessarResultado(medicoResult.ToResult(), medicoViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultadoSelecao = await servicoMedico.SelecionarPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var medicoResult = await servicoMedico.ExcluirAsync(resultadoSelecao.Value);

            return ProcessarResultado(medicoResult);
        }
    }
}
