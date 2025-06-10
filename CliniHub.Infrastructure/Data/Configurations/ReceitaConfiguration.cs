using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Data.Configurations;

public class ReceitaConfiguration : IEntityTypeConfiguration<Receita>
{
    public void Configure(EntityTypeBuilder<Receita> builder)
    {
        builder.ToTable("Receitas");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Conteudo)
            .IsRequired();

        builder.Property(r => r.Validade)
            .IsRequired();

        builder.Property(r => r.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.HasOne(r => r.Consulta)
            .WithMany(c => c.Receitas)
            .HasForeignKey(r => r.ConsultaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
