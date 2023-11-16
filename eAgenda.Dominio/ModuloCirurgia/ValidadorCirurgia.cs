using FluentValidation;

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
        }
    }
}
