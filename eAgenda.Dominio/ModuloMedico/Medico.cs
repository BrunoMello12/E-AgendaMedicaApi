using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloConsulta;

namespace eAgenda.Dominio.ModuloMedico
{
    public class Medico : EntidadeBase<Medico>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Disponivel { get; set; }
        public string CRM { get; private set; }
        public List<Cirurgia> Cirurgias { get; set; }
        public List<Consulta> Consultas { get; set; }

        public Medico()
        {
            
        }

        public Medico(string nome, string telefone, bool disponivel, string cRM, List<Cirurgia> cirurgias, List<Consulta> consultas)
        {
            Nome = nome;
            Telefone = telefone;
            Disponivel = disponivel;
            CRM = cRM;
            Cirurgias = cirurgias;
            Consultas = consultas;
        }

        public override void Atualizar(Medico registro)
        {
            Nome = registro.Nome;
            Telefone = registro.Telefone;
            Disponivel = registro.Disponivel;
            CRM = registro.CRM;
        }
    }
}
