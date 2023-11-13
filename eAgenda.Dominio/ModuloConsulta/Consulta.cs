using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloMedico;

namespace eAgenda.Dominio.ModuloConsulta
{
    public class Consulta : EntidadeBase<Consulta>
    {
        public string Titulo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public Medico Medico { get; set; }

        public Consulta(string titulo, TimeSpan horaInicio, TimeSpan horaTermino, Medico medico)
        {
            Titulo = titulo;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            Medico = medico;
        }

        public override void Atualizar(Consulta registro)
        {
            Titulo = registro.Titulo;
            HoraInicio = registro.HoraInicio;
            HoraTermino = registro.HoraTermino;
            Medico = registro.Medico;
        }
    }
}
