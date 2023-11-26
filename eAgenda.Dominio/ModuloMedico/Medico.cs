using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloConsulta;

namespace eAgenda.Dominio.ModuloMedico
{
    public class Medico : EntidadeBase<Medico>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CRM { get; set; }
        public List<Cirurgia> Cirurgias { get; set; }
        public List<Consulta> Consultas { get; set; }

        public Medico()
        {
            Cirurgias = new List<Cirurgia>();
            Consultas = new List<Consulta>();
        }

        public Medico(string nome, string telefone, string cRM) : this()
        {
            Nome = nome;
            Telefone = telefone;
            CRM = cRM;
        }

        public override void Atualizar(Medico registro)
        {
            Nome = registro.Nome;
            Telefone = registro.Telefone;
            CRM = registro.CRM;
        }
    }
}
