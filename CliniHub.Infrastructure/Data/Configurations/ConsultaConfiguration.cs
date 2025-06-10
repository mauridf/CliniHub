using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Data.Configurations;

public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
{
    public void Configure(EntityTypeBuilder<Consulta> builder)
    {
        builder.HasOne(c => c.Agendamento)
               .WithOne(a => a.Consulta)
               .HasForeignKey<Consulta>(c => c.AgendamentoId);
    }
}