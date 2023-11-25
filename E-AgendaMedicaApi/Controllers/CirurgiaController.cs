using AutoMapper;
using E_AgendaMedicaApi.Controllers.Shared;
using E_AgendaMedicaApi.ViewModels.ModuloCirurgia;
using E_AgendaMedicaApi.ViewModels.ModuloMedico;
using eAgenda.Aplicacao.ModuloCirurgia;
using eAgenda.Dominio.ModuloCirurgia;
using Microsoft.AspNetCore.Mvc;

namespace E_AgendaMedicaApi.Controllers
{
    [Route("api/cirurgias")]
    [ApiController]
    public class CirurgiaController : ApiControllerBase
    {
        private readonly ServicoCirurgia servicoCirurgia;
        private readonly IMapper mapeador;

        public CirurgiaController(ServicoCirurgia servicoCirurgia, IMapper mapeadorCirurgia)
        {
            this.servicoCirurgia = servicoCirurgia;
            this.mapeador = mapeadorCirurgia;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var cirurgiaResult = await servicoCirurgia.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarCirurgiaViewModel>>(cirurgiaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("medicos/{id}")]
        [ProducesResponseType(typeof(ListarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodosMedicosCirurgia(Guid id)
        {
            var cirurgiaResult = await servicoCirurgia.SelecionarPorIdAsync(id);

            if (cirurgiaResult.IsFailed)
                return NotFound(cirurgiaResult.Errors);

            var viewModel = mapeador.Map<List<ListarMedicoViewModel>>(cirurgiaResult.Value.Medicos);

            return Ok(viewModel);
        }


        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarCompletoPorId(Guid id)
        {
            var cirurgiaResult = await servicoCirurgia.SelecionarPorIdAsync(id);

            if (cirurgiaResult.IsFailed)
                return NotFound(cirurgiaResult.Errors);

            var viewModel = mapeador.Map<VisualizarCirurgiaViewModel>(cirurgiaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormsCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var cirurgiaResult = await servicoCirurgia.SelecionarPorIdAsync(id);

            if (cirurgiaResult.IsFailed)
                return NotFound(cirurgiaResult.Errors);

            var viewModel = mapeador.Map<FormsCirurgiaViewModel>(cirurgiaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsCirurgiaViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(FormsCirurgiaViewModel cirurgiaViewModel)
        {
            var cirurgia = mapeador.Map<Cirurgia>(cirurgiaViewModel);

            var cirurgiaResult = await servicoCirurgia.InserirAsync(cirurgia);

            return ProcessarResultado(cirurgiaResult.ToResult(), cirurgiaViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, FormsCirurgiaViewModel cirurgiaViewModel)
        {
            var resultadoSelecao = await servicoCirurgia.SelecionarPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var cirurgia = mapeador.Map(cirurgiaViewModel, resultadoSelecao.Value);

            var cirurgiaResult = await servicoCirurgia.EditarAsync(cirurgia);

            return ProcessarResultado(cirurgiaResult.ToResult(), cirurgiaViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultadoSelecao = await servicoCirurgia.SelecionarPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var cirurgiaResult = await servicoCirurgia.ExcluirAsync(resultadoSelecao.Value);

            return ProcessarResultado(cirurgiaResult);
        }
    }
}
