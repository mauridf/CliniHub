using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CliniHub.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
        builder.Property(u => u.CPF).IsRequired().HasMaxLength(11);
        builder.Property(u => u.EnderecoCompleto).HasMaxLength(200);
        builder.Property(u => u.UF).HasMaxLength(2);
        builder.Property(u => u.CEP).HasMaxLength(8);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
    }
}