using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CliniHub.Infrastructure.Data.Configurations;

public class AtendenteConfiguration : IEntityTypeConfiguration<Atendente>
{
    public void Configure(EntityTypeBuilder<Atendente> builder)
    {
        builder.ToTable("Atendentes");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(a => a.AlteradoEm)
            .IsRequired(false);

        // Relacionamento com Usuario
        builder.HasOne(a => a.Usuario)
            .WithOne(u => u.Atendente)
            .HasForeignKey<Atendente>(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento com Clinica
        builder.HasOne(a => a.Clinica)
            .WithMany(c => c.Atendentes)
            .HasForeignKey(a => a.ClinicaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}