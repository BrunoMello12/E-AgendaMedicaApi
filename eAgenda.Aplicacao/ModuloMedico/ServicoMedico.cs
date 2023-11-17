using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloMedico;
using FluentResults;
using Serilog;

namespace eAgenda.Aplicacao.ModuloMedico
{
    public class ServicoMedico : ServicoBase<Medico, ValidadorMedico>
    {
        private IRepositorioMedico repositorioMedico;
        private IContextoPersistencia contextoPersistencia;

        public ServicoMedico(
            IRepositorioMedico repositorioMedico,
            IContextoPersistencia contexto)
        {
            this.repositorioMedico = repositorioMedico;
            this.contextoPersistencia = contexto;
        }

        public async Task<Result<Medico>> InserirAsync(Medico medico)
        {
            Result resultado = Validar(medico);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            await repositorioMedico.InserirAsync(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(medico);
        }

        public async Task<Result<Medico>> EditarAsync(Medico medico)
        {
            var resultado = Validar(medico);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioMedico.Editar(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(medico);
        }

        public async Task<Result> ExcluirAsync(Guid id)
        {
            var medicoResult = await SelecionarPorIdAsync(id);

            if (medicoResult.IsSuccess)
                return await ExcluirAsync(medicoResult.Value);

            return Result.Fail(medicoResult.Errors);
        }

        public async Task<Result> ExcluirAsync(Medico medico)
        {
            repositorioMedico.Excluir(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Medico>>> SelecionarTodosAsync()
        {
            var medicos = await repositorioMedico.SelecionarTodosAsync();

            return Result.Ok(medicos);
        }

        public async Task<Result<Medico>> SelecionarPorIdAsync(Guid id)
        {
            var medico = await repositorioMedico.SelecionarPorIdAsync(id);

            if (medico == null)
            {
                Log.Logger.Warning($"Medico {id} não encontrado", id);

                return Result.Fail($"Medico {id} não encontrado");
            }

            return Result.Ok(medico);
        }
    }
}
