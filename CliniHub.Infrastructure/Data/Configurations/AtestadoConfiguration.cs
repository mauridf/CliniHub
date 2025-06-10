using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Data.Configurations;

public class AtestadoConfiguration : IEntityTypeConfiguration<Atestado>
{
    public void Configure(EntityTypeBuilder<Atestado> builder)
    {
        builder.ToTable("Atestados");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Conteudo)
            .IsRequired();

        builder.Property(a => a.Validade)
            .IsRequired();

        builder.Property(a => a.CriadoEm)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.HasOne(a => a.Consulta)
            .WithMany(c => c.Atestados)
            .HasForeignKey(a => a.ConsultaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
