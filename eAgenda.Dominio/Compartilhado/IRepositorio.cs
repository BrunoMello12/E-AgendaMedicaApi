namespace eAgenda.Dominio.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        Task<bool> InserirAsync(T novoRegistro);
        Task<bool> EditarAsync(T registroExistente);
        Task<bool> ExcluirAsync(Guid id);
        Task<bool> ExcluirAsync(T registroExistente);
        Task<List<T>> SelecionarTodosAsync();
        Task<T> SelecionarPorIdAsync(Guid numero);
    }
}
