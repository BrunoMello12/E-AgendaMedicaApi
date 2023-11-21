namespace eAgenda.Dominio.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        Task<bool> InserirAsync(T novoRegistro);
        void Editar(T registroExistente);
        void Excluir(T registroExistente);

        T SelecionarPorId(Guid id);

        Task<List<T>> SelecionarTodosAsync();
        Task<T> SelecionarPorIdAsync(Guid numero);
    }
}
