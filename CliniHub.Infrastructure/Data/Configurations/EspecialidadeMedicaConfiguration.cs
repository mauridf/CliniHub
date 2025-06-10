using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CliniHub.Infrastructure.Data.Configurations;

public class EspecialidadeMedicaConfiguration : IEntityTypeConfiguration<EspecialidadeMedica>
{
    public void Configure(EntityTypeBuilder<EspecialidadeMedica> builder)
    {
        builder.ToTable("EspecialidadesMedicas");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Observacao)
            .HasMaxLength(500);

        builder.Property(e => e.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(e => e.AlteradoEm)
            .IsRequired(false);

        // Índice para nome da especialidade
        builder.HasIndex(e => e.Nome)
            .IsUnique();
    }
}