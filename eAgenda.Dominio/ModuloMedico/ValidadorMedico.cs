using FluentValidation;

namespace eAgenda.Dominio.ModuloMedico
{
    public class ValidadorMedico : AbstractValidator<Medico>
    {
        public ValidadorMedico()
        {
            RuleFor(x => x.Nome)
               .NotNull().NotEmpty().MinimumLength(3).Matches("^[A-Za-zÀ-ÿ ]+$");

            RuleFor(x => x.CRM)
               .CrmMedico()
               .NotNull().NotEmpty();

            RuleFor(x => x.Telefone)
               .Telefone()
               .NotNull().NotEmpty();
        }
    }
}
