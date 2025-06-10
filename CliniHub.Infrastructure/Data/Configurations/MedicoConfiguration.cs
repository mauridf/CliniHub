using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CliniHub.Infrastructure.Data.Configurations;

public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> builder)
    {
        builder.ToTable("Medicos");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.CRM)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(m => m.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(m => m.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(m => m.AlteradoEm)
            .IsRequired(false);

        // Relacionamento com Usuario
        builder.HasOne(m => m.Usuario)
            .WithOne(u => u.Medico)
            .HasForeignKey<Medico>(m => m.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento com Clinica
        builder.HasOne(m => m.Clinica)
            .WithMany(c => c.Medicos)
            .HasForeignKey(m => m.ClinicaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento com EspecialidadeMedica
        builder.HasOne(m => m.EspecialidadeMedica)
            .WithMany(e => e.Medicos)
            .HasForeignKey(m => m.EspecialidadeMedicaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}