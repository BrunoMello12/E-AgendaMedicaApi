using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace E_AgendaMedicaApi.Filters
{
    public class CustomSwaggerDateFormatFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var schema in swaggerDoc.Components.Schemas)
            {
                if (schema.Value.Properties != null)
                {
                    foreach (var property in schema.Value.Properties)
                    {
                        if (property.Value.Type == "string" && property.Value.Format == "date-time")
                        {
                            property.Value.Example = new OpenApiString(DateTime.UtcNow.ToString("yyyy-MM-dd"));
                        }
                    }
                }
            }
        }
    }
}
