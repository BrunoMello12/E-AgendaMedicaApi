using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloConsulta;
using eAgenda.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infra.Orm.ModuloConsulta
{
    public class RepositorioConsultaOrm : RepositorioBase<Consulta>, IRepositorioConsulta
    {
        public RepositorioConsultaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }

        public async Task<List<Consulta>> SelecionarConsultasMedico(Guid id)
        {
            return await registros.Where(x => x.Medico.Id == id).ToListAsync();
        }

        public override async Task<Consulta> SelecionarPorIdAsync(Guid id)
        {
            return await registros.Include(x => x.Medico)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<List<Consulta>> SelecionarTodosAsync()
        {
            return await registros.Include(x => x.Medico).ToListAsync();
        }
    }
}
