using Microsoft.OpenApi.Models;

namespace Produto.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Produtos", Version = "v1" });
            });
        }
    }
}
