using FluentValidation;

namespace eAgenda.Dominio.ModuloConsulta
{
    public class ValidadorConsulta : AbstractValidator<Consulta>
    {
        public ValidadorConsulta()
        {
            RuleFor(x => x.Titulo)
                .NotNull().NotEmpty();

            RuleFor(x => x.HoraInicio)
                .NotEmpty();

            RuleFor(x => x.HoraTermino)
                .NotEmpty();

            RuleFor(x => x.MedicoId)
                .NotNull().WithMessage("A consulta deve ter um médico.");
        }
    }
}
