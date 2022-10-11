using Microsoft.EntityFrameworkCore;
using Produto.API.Data;

namespace Produto.API.Configuration
{
    public static class EntityFrameworkConfig
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProdutoContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("ProdutoConnection"))
            );
        }
    }
}
