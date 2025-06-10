using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CliniHub.Infrastructure.Data.Configurations;

public class BloqueioAgendaMedicoConfiguration : IEntityTypeConfiguration<BloqueioAgendaMedico>
{
    public void Configure(EntityTypeBuilder<BloqueioAgendaMedico> builder)
    {
        builder.ToTable("BloqueiosAgendaMedicos");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Motivo)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(b => b.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(b => b.AlteradoEm)
            .IsRequired(false);

        // Relacionamento com Medico
        builder.HasOne(b => b.Medico)
            .WithMany(m => m.BloqueiosAgenda)
            .HasForeignKey(b => b.MedicoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento com Clinica
        builder.HasOne(b => b.Clinica)
            .WithMany(c => c.BloqueiosAgendaMedicos)
            .HasForeignKey(b => b.ClinicaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Verificação para garantir que DataHoraFim > DataHoraInicio
        builder.HasCheckConstraint("CK_BloqueioAgendaMedico_DataHora",
            "\"DataHoraFim\" > \"DataHoraInicio\"");
    }
}