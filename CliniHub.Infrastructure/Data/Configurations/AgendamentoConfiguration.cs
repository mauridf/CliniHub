using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CliniHub.Infrastructure.Data.Configurations;

public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
{
    public void Configure(EntityTypeBuilder<Agendamento> builder)
    {
        builder.ToTable("Agendamentos");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.DataHora)
            .IsRequired();

        builder.Property(a => a.Observacao)
            .HasMaxLength(500);

        builder.Property(a => a.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(a => a.AlteradoEm)
            .IsRequired(false);

        // Relacionamento com Paciente
        builder.HasOne(a => a.Paciente)
            .WithMany(p => p.Agendamentos)
            .HasForeignKey(a => a.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Clinica
        builder.HasOne(a => a.Clinica)
            .WithMany(c => c.Agendamentos)
            .HasForeignKey(a => a.ClinicaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Medico
        builder.HasOne(a => a.Medico)
            .WithMany(m => m.Agendamentos)
            .HasForeignKey(a => a.MedicoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Consulta (1:1)
        builder.HasOne(a => a.Consulta)
            .WithOne(c => c.Agendamento)
            .HasForeignKey<Consulta>(c => c.AgendamentoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Índice composto para evitar agendamentos duplicados
        builder.HasIndex(a => new { a.MedicoId, a.DataHora })
            .IsUnique();
    }
}