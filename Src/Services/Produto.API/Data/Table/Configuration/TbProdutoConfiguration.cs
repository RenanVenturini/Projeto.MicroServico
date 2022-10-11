using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Produto.API.Data.Table.Configuration
{
    public class TbProdutoConfiguration : IEntityTypeConfiguration<TbProduto>
    {
        public void Configure(EntityTypeBuilder<TbProduto> builder)
        {
            builder.ToTable("TbProduto", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().UseIdentityColumn();
            builder.Property(x => x.Nome).HasColumnName(@"Nome").HasColumnType("varchar").IsRequired().HasColumnType("varchar").IsUnicode(false).HasMaxLength(200);
            builder.Property(x => x.Preco).HasColumnName(@"Preco").HasColumnType("decimal").IsRequired().HasColumnType("decimal").HasPrecision(18, 2);
        }

    }
}
