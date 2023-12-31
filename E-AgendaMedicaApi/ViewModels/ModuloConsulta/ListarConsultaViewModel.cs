﻿namespace E_AgendaMedicaApi.ViewModels.ModuloConsulta
{
    public class ListarConsultaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public string NomeMedico { get; set; }
    }
}
