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

        public Medico(string nome, string telefone, string cRM)
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
