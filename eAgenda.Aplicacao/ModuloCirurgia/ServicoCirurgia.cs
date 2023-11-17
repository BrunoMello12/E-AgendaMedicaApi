using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCirurgia;
using FluentResults;
using Serilog;

namespace eAgenda.Aplicacao.ModuloCirurgia
{
    public class ServicoCirurgia : ServicoBase<Cirurgia, ValidadorCirurgia>
    {
        private IRepositorioCirurgia repositorioCirurgia;
        private IContextoPersistencia contextoPersistencia;

        public ServicoCirurgia(
            IRepositorioCirurgia repositorioCirurgia,
            IContextoPersistencia contexto)
        {
            this.repositorioCirurgia = repositorioCirurgia;
            this.contextoPersistencia = contexto;
        }

        public async Task<Result<Cirurgia>> InserirAsync(Cirurgia cirurgia)
        {
            Result resultado = Validar(cirurgia);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            await repositorioCirurgia.InserirAsync(cirurgia);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(cirurgia);
        }

        public async Task<Result<Cirurgia>> EditarAsync(Cirurgia cirurgia)
        {
            var resultado = Validar(cirurgia);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioCirurgia.Editar(cirurgia);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(cirurgia);
        }

        public async Task<Result> ExcluirAsync(Guid id)
        {
            var cirurgiaResult = await SelecionarPorIdAsync(id);

            if (cirurgiaResult.IsSuccess)
                return await ExcluirAsync(cirurgiaResult.Value);

            return Result.Fail(cirurgiaResult.Errors);
        }

        public async Task<Result> ExcluirAsync(Cirurgia cirurgia)
        {
            repositorioCirurgia.Excluir(cirurgia);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Cirurgia>>> SelecionarTodosAsync()
        {
            var cirurgias = await repositorioCirurgia.SelecionarTodosAsync();

            return Result.Ok(cirurgias);
        }

        public async Task<Result<Cirurgia>> SelecionarPorIdAsync(Guid id)
        {
            var cirurgia = await repositorioCirurgia.SelecionarPorIdAsync(id);

            if (cirurgia == null)
            {
                Log.Logger.Warning($"Cirurgia {id} não encontrada", id);

                return Result.Fail($"Cirurgia {id} não encontrada");
            }

            return Result.Ok(cirurgia);
        }
    }
}
