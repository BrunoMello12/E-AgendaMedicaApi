using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Dominio.ModuloCirurgia
{
    public class ValidadorCirurgia : AbstractValidator<Cirurgia>
    {
        public ValidadorCirurgia()
        {
            RuleFor(x => x.Titulo)
               .NotNull().NotEmpty();

            RuleFor(x => x.HoraInicio)
                .NotEmpty();

            RuleFor(x => x.HoraTermino)
                .NotEmpty();

            RuleFor(x => x.Medicos)
                .NotNull().NotEmpty().WithMessage("A cirurgia deve ter pelo menos um médico.")
                .Must(medicos => medicos != null && medicos.Count > 0).WithMessage("A cirurgia deve ter pelo menos um médico.");
        }
    }
}
