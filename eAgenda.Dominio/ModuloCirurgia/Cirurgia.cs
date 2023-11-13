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

        public Cirurgia(string titulo, TimeSpan horaInicio, TimeSpan horaTermino, List<Medico> medicos)
        {
            Titulo = titulo;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            Medicos = medicos;
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
