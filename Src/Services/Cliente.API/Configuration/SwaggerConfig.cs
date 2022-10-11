using Microsoft.OpenApi.Models;

namespace Cliente.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clientes", Version = "v1" });
            });
        }
    }
}
