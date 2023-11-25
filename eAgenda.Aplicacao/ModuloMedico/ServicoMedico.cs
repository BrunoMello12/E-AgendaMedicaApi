using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloConsulta;
using eAgenda.Dominio.ModuloMedico;
using FluentResults;
using Serilog;

namespace eAgenda.Aplicacao.ModuloMedico
{
    public class ServicoMedico : ServicoBase<Medico, ValidadorMedico>
    {
        private IRepositorioMedico repositorioMedico;
        private IRepositorioConsulta repositorioConsulta;
        private IRepositorioCirurgia repositorioCirurgia;
        private IContextoPersistencia contextoPersistencia;

        public ServicoMedico(
            IRepositorioMedico repositorioMedico,
            IRepositorioConsulta repositorioConsulta,
            IRepositorioCirurgia repositorioCirurgia,
            IContextoPersistencia contexto)
        {
            this.repositorioMedico = repositorioMedico;
            this.contextoPersistencia = contexto;
            this.repositorioConsulta = repositorioConsulta;
            this.repositorioCirurgia = repositorioCirurgia;
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

        public async Task<Result<List<Consulta>>> SelecionarConsultasMedicoAsync(Guid id)
        {
            var consultas = await repositorioConsulta.SelecionarConsultasMedico(id);

            if (consultas == null)
            {
                Log.Logger.Warning($"Nenhuma Consulta encontrada");

                return Result.Fail($"Nenhuma Consulta encontrada");
            }

            return Result.Ok(consultas);
        }

        public async Task<Result<List<Cirurgia>>> SelecionarCirurgiasMedicoAsync(Guid id)
        {
            var cirurgias = await repositorioCirurgia.SelecionarCirurgiasMedico(id);

            if (cirurgias == null)
            {
                Log.Logger.Warning($"Nenhuma Cirurgia encontrada");

                return Result.Fail($"Nenhuma Cirurgia encontrada");
            }

            return Result.Ok(cirurgias);
        }
    }
}
