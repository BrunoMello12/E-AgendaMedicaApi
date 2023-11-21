using AutoMapper;
using E_AgendaMedicaApi.Controllers.Shared;
using E_AgendaMedicaApi.ViewModels.ModuloConsulta;
using eAgenda.Aplicacao.ModuloConsulta;
using eAgenda.Dominio.ModuloConsulta;
using Microsoft.AspNetCore.Mvc;

namespace E_AgendaMedicaApi.Controllers
{
    [Route("api/consultas")]
    [ApiController]
    public class ConsultaController : ApiControllerBase
    {
        private readonly ServicoConsulta servicoConsulta;
        private readonly IMapper mapeador;

        public ConsultaController(ServicoConsulta servicoConsulta, IMapper mapeadorConsulta)
        {
            this.servicoConsulta = servicoConsulta;
            this.mapeador = mapeadorConsulta;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var consultaResult = await servicoConsulta.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarConsultaViewModel>>(consultaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarCompletoPorId(Guid id)
        {
            var consultaResult = await servicoConsulta.SelecionarPorIdAsync(id);

            if (consultaResult.IsFailed)
                return NotFound(consultaResult.Errors);

            var viewModel = mapeador.Map<VisualizarConsultaViewModel>(consultaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var consultaResult = await servicoConsulta.SelecionarPorIdAsync(id);

            if (consultaResult.IsFailed)
                return NotFound(consultaResult.Errors);

            var viewModel = mapeador.Map<FormsConsultaViewModel>(consultaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(FormsConsultaViewModel consultaViewModel)
        {
            var consulta = mapeador.Map<Consulta>(consultaViewModel);

            var consultaResult = await servicoConsulta.InserirAsync(consulta);

            return ProcessarResultado(consultaResult.ToResult(), consultaViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, FormsConsultaViewModel consultaViewModel)
        {
            var resultadoSelecao = await servicoConsulta.SelecionarPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var consulta = mapeador.Map(consultaViewModel, resultadoSelecao.Value);

            var consultaResult = await servicoConsulta.EditarAsync(consulta);

            return ProcessarResultado(consultaResult.ToResult(), consultaViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultadoSelecao = await servicoConsulta.SelecionarPorIdAsync(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(resultadoSelecao.Errors);

            var consultaResult = await servicoConsulta.ExcluirAsync(resultadoSelecao.Value);

            return ProcessarResultado(consultaResult);
        }
    }
}
