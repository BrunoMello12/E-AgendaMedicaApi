using FluentValidation;

namespace eAgenda.Dominio
{
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// //https://medium.com/@igorrozani/criando-uma-express%C3%A3o-regular-para-telefone-fef7a8f98828
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilder<T, string> Telefone<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Matches(@"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})");

            return options;
        }
        public static IRuleBuilder<T, string> CrmMedico<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Matches(@"^\d{5}-[A-Za-z]{2}$");

            return options;
        }
    }
}
