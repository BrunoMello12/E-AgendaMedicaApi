using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloMedico;

namespace eAgenda.Dominio.ModuloCirurgia
{
    public class Cirurgia : EntidadeBase<Cirurgia>
    {
        public string Titulo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public List<Medico> Medicos { get; set; }

        public Cirurgia()
        {
            Medicos = new List<Medico>();
        }

        public Cirurgia(string titulo, TimeSpan horaInicio, TimeSpan horaTermino, List<Medico> medicos)
        {
            Titulo = titulo;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
        }

        public bool AdicionarMedico(Medico medico)
        {
            if (Medicos.Exists(x => x.Equals(medico)) == false)
            {
                medico.Cirurgias.Add(this);
                Medicos.Add(medico);

                return true;
            }

            return false;
        }

        public void RemoverMedico(Guid medicoId)
        {
            var medicoCirurgia = Medicos.Find(x => x.Id.Equals(medicoId));

            Medicos.Remove(medicoCirurgia);
        }



        public override void Atualizar(Cirurgia registro)
        {
            Titulo = registro.Titulo;
            HoraInicio = registro.HoraInicio;
            HoraTermino = registro.HoraTermino;
            Medicos = registro.Medicos;
        }
    }
}
