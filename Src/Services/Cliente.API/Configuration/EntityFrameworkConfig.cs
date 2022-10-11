using Cliente.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Cliente.API.Configuration
{
    public static class EntityFrameworkConfig
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClienteContext>(options 
                => options.UseSqlServer(configuration
                .GetConnectionString("ClienteConnection")));
        }
    }
}
