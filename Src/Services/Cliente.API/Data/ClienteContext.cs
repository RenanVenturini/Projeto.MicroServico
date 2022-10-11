using Cliente.API.Data.Table;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cliente.API.Data
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options) { }

        public DbSet<TbCliente> Clientes { get; set; }
        public DbSet<TbEnderecoCliente> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
