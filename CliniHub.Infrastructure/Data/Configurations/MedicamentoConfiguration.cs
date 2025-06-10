using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Data.Configurations;

public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
{
    public void Configure(EntityTypeBuilder<Medicamento> builder)
    {
        builder.ToTable("Medicamentos");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.NomeComercial)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.PrincipioAtivo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Apresentacao)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.HasIndex(m => m.NomeComercial)
            .IsUnique();
    }
}