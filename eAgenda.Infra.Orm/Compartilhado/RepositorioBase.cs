using eAgenda.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infra.Orm.Compartilhado
{
    public class RepositorioBase<TEntity> where TEntity : EntidadeBase<TEntity>
    {
        protected DbSet<TEntity> registros;
        private readonly eAgendaDbContext dbContext;

        public RepositorioBase(IContextoPersistencia contextoPersistencia)
        {
            dbContext = (eAgendaDbContext)contextoPersistencia;
            registros = dbContext.Set<TEntity>();
        }

        public virtual async Task<bool> InserirAsync(TEntity novoRegistro)
        {
            await registros.AddAsync(novoRegistro);
            return true;
        }

        public virtual void Editar(TEntity registro)
        {
            registros.Update(registro);
        }

        public virtual void Excluir(TEntity registro)
        {
            registros.Remove(registro);
        }

        public virtual async Task<TEntity> SelecionarPorIdAsync(Guid id)
        {
            return await registros
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<TEntity>> SelecionarTodosAsync()
        {
            return await registros.ToListAsync();
        }
    }
}
