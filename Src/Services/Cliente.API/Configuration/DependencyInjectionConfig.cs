using Cliente.API.Data.Interfaces;
using Cliente.API.Data.Repository;
using Cliente.API.Services;
using Cliente.API.Services.Interfaces;

namespace Cliente.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteService, ClienteService>();
        }
    }
}
