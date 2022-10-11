using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cliente.API.Data.Table.Configuration
{
    public class TbClienteConfiguration : IEntityTypeConfiguration<TbCliente>
    {
        public void Configure(EntityTypeBuilder<TbCliente> builder)
        {
            builder.ToTable("TbCliente", "dbo");
            builder.HasKey(x => x.ClienteId);

            builder.Property(x => x.ClienteId).HasColumnName(@"ClienteId").HasColumnType("int").IsRequired().UseIdentityColumn();
            builder.Property(x => x.Nome).HasColumnName(@"Nome").HasColumnType("varchar").IsRequired().HasColumnType("varchar").IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.CPF).HasColumnName(@"CPF").HasColumnType("varchar").IsRequired().HasColumnType("varchar").IsUnicode(false).HasMaxLength(14);

            builder.HasOne(x => x.Endereco).WithOne(x => x.Cliente).HasForeignKey<TbEnderecoCliente>(x => x.ClienteId);

        }
    }
}
