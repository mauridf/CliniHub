using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CliniHub.Infrastructure.Data.Configurations;

public class DisponibilidadeMedicoConfiguration : IEntityTypeConfiguration<DisponibilidadeMedico>
{
    public void Configure(EntityTypeBuilder<DisponibilidadeMedico> builder)
    {
        builder.ToTable("DisponibilidadesMedicos");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(d => d.AlteradoEm)
            .IsRequired(false);

        // Relacionamento com Medico
        builder.HasOne(d => d.Medico)
            .WithMany(m => m.Disponibilidades)
            .HasForeignKey(d => d.MedicoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento com Clinica
        builder.HasOne(d => d.Clinica)
            .WithMany(c => c.DisponibilidadesMedicos)
            .HasForeignKey(d => d.ClinicaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Índice composto para evitar duplicidade de disponibilidade
        builder.HasIndex(d => new { d.MedicoId, d.ClinicaId, d.DiaSemana })
            .IsUnique();
    }
}