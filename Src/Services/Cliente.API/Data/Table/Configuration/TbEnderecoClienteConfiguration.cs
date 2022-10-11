using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cliente.API.Data.Table.Configuration
{
    public class TbEnderecoClienteConfiguration : IEntityTypeConfiguration<TbEnderecoCliente>
    {
        public void Configure(EntityTypeBuilder<TbEnderecoCliente> builder)
        {
            builder.ToTable("TbEnderecoCliente", "dbo");
            builder.HasKey(x => x.EnderecoId);

            builder.Property(x => x.EnderecoId).HasColumnName(@"EnderecoId").HasColumnType("int").IsRequired().UseIdentityColumn();
            builder.Property(x => x.ClienteId).HasColumnName(@"ClienteId").HasColumnType("int").IsRequired();
            builder.Property(x => x.Logradouro).HasColumnName(@"Logradouro").HasColumnType("varchar").IsRequired().HasColumnType("varchar").IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.Numero).HasColumnName(@"Numero").HasColumnType("varchar").IsRequired().HasColumnType("varchar").IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.Complemento).HasColumnName(@"Complemento").HasColumnType("varchar").IsRequired().HasColumnType("varchar").IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Cidade).HasColumnName(@"Cidade").HasColumnType("varchar").IsRequired().HasColumnType("varchar").IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Bairro).HasColumnName(@"Bairro").HasColumnType("varchar").IsRequired().HasColumnType("varchar").IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.UF).HasColumnName(@"UF").HasColumnType("varchar").IsRequired().HasColumnType("varchar").IsUnicode(false).HasMaxLength(2);
            builder.Property(x => x.CEP).HasColumnName(@"CEP").HasColumnType("varchar").IsRequired().HasColumnType("varchar").IsUnicode(false).HasMaxLength(9);

        }
    }
}
