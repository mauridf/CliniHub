using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CliniHub.Infrastructure.Data.Configurations;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.EnderecoCompleto)
            .HasMaxLength(200);

        builder.Property(p => p.UF)
            .HasMaxLength(2);

        builder.Property(p => p.CEP)
            .HasMaxLength(8);

        builder.Property(p => p.Telefone)
            .HasMaxLength(20);

        builder.Property(p => p.Email)
            .HasMaxLength(100);

        builder.Property(p => p.DataNascimento)
            .IsRequired();

        builder.Property(p => p.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(p => p.AlteradoEm)
            .IsRequired(false);

        builder.HasIndex(p => p.CPF).IsUnique();
    }
}