namespace eAgenda.Dominio.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        Task<bool> InserirAsync(T novoRegistro);
        Task<bool> EditarAsync(T novoRegistro);
        Task<bool> ExcluirAsync(T novoRegistro);
        Task<List<T>> SelecionarTodosAsync();
        Task<T> SelecionarPorIdAsync(Guid numero);
    }
}
