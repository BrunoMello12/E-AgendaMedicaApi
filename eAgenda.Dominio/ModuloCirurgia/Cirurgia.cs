using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloConsulta;
using eAgenda.Dominio.ModuloMedico;

namespace eAgenda.Dominio.ModuloCirurgia
{
    public class Cirurgia : EntidadeBase<Cirurgia>
    {
        public string Titulo { get; set; }
        public DateTime Data { get; set; }
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

        public void CalcularDescanso()
        {
            TimeSpan periodoDescanso = TimeSpan.FromMinutes(20);

            HoraTermino.Add(periodoDescanso);
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
