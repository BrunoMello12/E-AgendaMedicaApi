namespace eAgenda.Dominio.Compartilhado
{
    public interface IContextoPersistencia
    {
        Task<bool> GravarDadosAsync();
    }
}
