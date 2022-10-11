using Produto.API.Data.Interfaces;
using Produto.API.Data.Repository;
using Produto.API.Services;
using Produto.API.Services.Interfaces;

namespace Produto.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();
        }
    }
}
