using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.ModuloMedico
{
    public class Medico : EntidadeBase<Medico>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Disponivel { get; set; }
        public string CRM { get; private set; }

        public Medico(string nome, string telefone, bool disponivel, string cRM)
        {
            Nome = nome;
            Telefone = telefone;
            Disponivel = disponivel;
            CRM = cRM;
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
