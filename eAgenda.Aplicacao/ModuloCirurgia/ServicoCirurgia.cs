using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloConsulta;
using FluentResults;
using Serilog;

namespace eAgenda.Aplicacao.ModuloCirurgia
{
    public class ServicoCirurgia : ServicoBase<Cirurgia, ValidadorCirurgia>
    {
        private IRepositorioCirurgia repositorioCirurgia;
        private IRepositorioConsulta repositorioConsulta;
        private IContextoPersistencia contextoPersistencia;

        public ServicoCirurgia(
            IRepositorioCirurgia repositorioCirurgia,
            IRepositorioConsulta repositorioConsulta,
            IContextoPersistencia contexto)
        {
            this.repositorioCirurgia = repositorioCirurgia;
            this.repositorioConsulta = repositorioConsulta;
            this.contextoPersistencia = contexto;
        }

        public async Task<Result<Cirurgia>> InserirAsync(Cirurgia cirurgia)
        {
            if (cirurgia.HoraInicio >= cirurgia.HoraTermino)
                return Result.Fail("A hora início não pode ser maior que a hora término");

            TimeSpan horarioLimite = new TimeSpan(19, 50, 0);

            if (cirurgia.HoraTermino > horarioLimite)
                return Result.Fail("O horário término limite é 19:50");

            TimeSpan periodoDescanso = TimeSpan.FromHours(4);

            cirurgia.HoraTermino += periodoDescanso;

            var medicosIds = cirurgia.Medicos.Select(m => m.Id).ToList();

            foreach (var medicoId in medicosIds)
            {
                var JaExisteConsulta = await repositorioConsulta.ExisteConsultaNesseHorarioPorMedicoId(medicoId, cirurgia.HoraInicio, cirurgia.HoraTermino, cirurgia.Data, cirurgia.Id);

                var JaExisteCirurgia = await repositorioCirurgia.ExisteCirurgiasNesseHorarioPorMedicoId(medicoId, cirurgia.HoraInicio, cirurgia.HoraTermino, cirurgia.Data, cirurgia.Id);

                if (JaExisteConsulta || JaExisteCirurgia)
                    return Result.Fail("Horário indísponivel");
            }

            Result resultado = Validar(cirurgia);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            await repositorioCirurgia.InserirAsync(cirurgia);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(cirurgia);
        }

        public async Task<Result<Cirurgia>> EditarAsync(Cirurgia cirurgia)
        {
            if (cirurgia.HoraInicio >= cirurgia.HoraTermino)
                return Result.Fail("A hora início não pode ser maior que a hora término");

            TimeSpan horarioLimite = new TimeSpan(19, 50, 0);

            if (cirurgia.HoraTermino > horarioLimite)
                return Result.Fail("O horário término limite é 19:50");

            TimeSpan periodoDescanso = TimeSpan.FromHours(4);

            cirurgia.HoraTermino += periodoDescanso;

            var medicosIds = cirurgia.Medicos.Select(m => m.Id).ToList();

            foreach (var medicoId in medicosIds)
            {
                var JaExisteConsulta = await repositorioConsulta.ExisteConsultaNesseHorarioPorMedicoId(medicoId, cirurgia.HoraInicio, cirurgia.HoraTermino, cirurgia.Data, cirurgia.Id);

                var JaExisteCirurgia = await repositorioCirurgia.ExisteCirurgiasNesseHorarioPorMedicoId(medicoId, cirurgia.HoraInicio, cirurgia.HoraTermino, cirurgia.Data, cirurgia.Id);

                if (JaExisteConsulta || JaExisteCirurgia)
                    return Result.Fail("Horário indísponivel");
            }

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
