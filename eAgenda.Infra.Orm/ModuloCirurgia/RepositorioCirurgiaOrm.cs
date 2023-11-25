using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloConsulta;
using eAgenda.Dominio.ModuloMedico;
using eAgenda.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infra.Orm.ModuloCirurgia
{
    public class RepositorioCirurgiaOrm : RepositorioBase<Cirurgia>, IRepositorioCirurgia
    {
        public RepositorioCirurgiaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }

        public async Task<List<Cirurgia>> SelecionarCirurgiasMedico(Guid id)
        {
            return await registros.Where(cirurgia => cirurgia.Medicos.Any(medico => medico.Id == id)).ToListAsync();
        }

        public override async Task<Cirurgia> SelecionarPorIdAsync(Guid id)
        {
            return registros.Include(x => x.Medicos).SingleOrDefault(x => x.Id == id);
        }

        public override async Task<List<Cirurgia>> SelecionarTodosAsync()
        {
            return await registros.Include(x => x.Medicos).ToListAsync();
        }
    }
}
