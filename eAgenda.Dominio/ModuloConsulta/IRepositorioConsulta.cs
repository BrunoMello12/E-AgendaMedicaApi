using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.ModuloConsulta
{
    public interface IRepositorioConsulta : IRepositorio<Consulta>
    {
        public Task<List<Consulta>> SelecionarConsultasMedico(Guid id);
    }
}
