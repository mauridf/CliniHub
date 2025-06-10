using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Data.Configurations;

public class TipoExameConfiguration : IEntityTypeConfiguration<TipoExame>
{
    public void Configure(EntityTypeBuilder<TipoExame> builder)
    {
        builder.ToTable("TiposExames");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Descricao)
            .HasMaxLength(500);

        builder.Property(t => t.Preparo)
            .HasMaxLength(500);

        builder.Property(t => t.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");
    }
}