using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CliniHub.Infrastructure.Data.Configurations;

public class LaudoConfiguration : IEntityTypeConfiguration<Laudo>
{
    public void Configure(EntityTypeBuilder<Laudo> builder)
    {
        builder.ToTable("Laudos");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.ConteudoLaudo)
            .IsRequired();

        builder.Property(l => l.LaudoPDF)
            .HasMaxLength(500);

        builder.Property(l => l.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(l => l.AlteradoEm)
            .IsRequired(false);

        // Relacionamento com Paciente
        builder.HasOne(l => l.Paciente)
            .WithMany(p => p.Laudos)
            .HasForeignKey(l => l.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Clinica
        builder.HasOne(l => l.Clinica)
            .WithMany(c => c.Laudos)
            .HasForeignKey(l => l.ClinicaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Medico
        builder.HasOne(l => l.Medico)
            .WithMany(m => m.Laudos)
            .HasForeignKey(l => l.MedicoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Agendamento (opcional)
        builder.HasOne(l => l.Agendamento)
            .WithOne(a => a.Laudo)
            .HasForeignKey<Laudo>(l => l.AgendamentoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}