﻿namespace E_AgendaMedicaApi.ViewModels.ModuloConsulta
{
    public class FormsConsultaViewModel
    {
        public string Titulo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public Guid MedicoId { get; set; }
    }
}
