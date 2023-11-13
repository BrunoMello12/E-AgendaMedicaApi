using FluentValidation;

namespace eAgenda.Dominio.ModuloMedico
{
    public class ValidadorMedico : AbstractValidator<Medico>
    {
        public ValidadorMedico()
        {
            RuleFor(x => x.Nome)
               .NotNull().NotEmpty();

            RuleFor(x => x.Telefone)
                .Telefone();

            RuleFor(x => x.CRM)
                .NotNull().NotEmpty();
        }
    }
}
