using E_AgendaMedicaApi.Config;
using E_AgendaMedicaApi.Config.AutomapperConfig;
using E_AgendaMedicaApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace E_AgendaMedicaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = false;
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });

                c.DocumentFilter<CustomSwaggerDateFormatFilter>();
            });

            builder.Services.ConfigurarSerilog(builder.Logging);
            builder.Services.ConfigurarAutoMapper();
            builder.Services.ConfigurarInjecaoDependencia(builder.Configuration);
            builder.Services.ConfigurarSwagger();
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("Desenvolvimento", servicos => servicos.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.ConfigurarControllers();

            var app = builder.Build();

            app.UseMiddleware<ManipuladorExcecoes>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                
            }

            app.UseCors("Desenvolvimento");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}