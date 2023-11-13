namespace E_AgendaMedicaApi.Config.AutomapperConfig
{
    public static class AutoMapperConfigExtension
    {
        public static void ConfigurarAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<MedicoProfile>();
                opt.AddProfile<ConsultaProfile>();
                opt.AddProfile<CirurgiaProfile>();
            });
        }
    }
}
