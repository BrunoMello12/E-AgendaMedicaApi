using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloConsulta;
using FluentResults;
using Serilog;

namespace eAgenda.Aplicacao.ModuloConsulta
{
    public class ServicoConsulta : ServicoBase<Consulta, ValidadorConsulta>
    {
        private IRepositorioConsulta repositorioConsulta;
        private IRepositorioCirurgia repositorioCirurgia;
        private IContextoPersistencia contextoPersistencia;

        public ServicoConsulta(
            IRepositorioConsulta repositorioConsulta,
            IRepositorioCirurgia repositorioCirurgia,
            IContextoPersistencia contexto)
        {
            this.repositorioConsulta = repositorioConsulta;
            this.repositorioCirurgia = repositorioCirurgia;
            this.contextoPersistencia = contexto;
        }

        public async Task<Result<Consulta>> InserirAsync(Consulta consulta)
        {
            TimeSpan periodoDescanso = TimeSpan.FromMinutes(20);

            consulta.HoraTermino += periodoDescanso;

            var JaExisteConsulta = await repositorioConsulta.ExisteConsultaNesseHorarioPorMedicoId(consulta.MedicoId, consulta.HoraInicio, consulta.HoraTermino, consulta.Data);

            var JaExisteCirurgia = await repositorioCirurgia.ExisteCirurgiasNesseHorarioPorMedicoId(consulta.MedicoId, consulta.HoraInicio, consulta.HoraTermino, consulta.Data);

            if (JaExisteConsulta || JaExisteCirurgia)
                return Result.Fail("Horário indísponivel");

            Result resultado = Validar(consulta);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            await repositorioConsulta.InserirAsync(consulta);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result<Consulta>> EditarAsync(Consulta consulta)
        {
            var resultado = Validar(consulta);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            repositorioConsulta.Editar(consulta);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result> ExcluirAsync(Guid id)
        {
            var consultaResult = await SelecionarPorIdAsync(id);

            if (consultaResult.IsSuccess)
                return await ExcluirAsync(consultaResult.Value);

            return Result.Fail(consultaResult.Errors);
        }

        public async Task<Result> ExcluirAsync(Consulta consulta)
        {
            repositorioConsulta.Excluir(consulta);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Consulta>>> SelecionarTodosAsync()
        {
            var consultas = await repositorioConsulta.SelecionarTodosAsync();

            return Result.Ok(consultas);
        }

        public async Task<Result<Consulta>> SelecionarPorIdAsync(Guid id)
        {
            var consulta = await repositorioConsulta.SelecionarPorIdAsync(id);

            if (consulta == null)
            {
                Log.Logger.Warning($"Consulta {id} não encontrada", id);

                return Result.Fail($"Consulta {id} não encontrada");
            }

            return Result.Ok(consulta);
        }
    }
}
