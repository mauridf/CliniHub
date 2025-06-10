using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CliniHub.Infrastructure.Data.Configurations;

public class ClinicaConfiguration : IEntityTypeConfiguration<Clinica>
{
    public void Configure(EntityTypeBuilder<Clinica> builder)
    {
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
        builder.Property(c => c.CNPJ).IsRequired().HasMaxLength(14);
        builder.Property(c => c.EnderecoCompleto).HasMaxLength(200);
        builder.Property(c => c.UF).HasMaxLength(2);
        builder.Property(c => c.CEP).HasMaxLength(8);
        builder.Property(c => c.Telefone).HasMaxLength(20);
        builder.Property(c => c.Email).HasMaxLength(100);
        builder.Property(c => c.Website).HasMaxLength(100);
    }
}