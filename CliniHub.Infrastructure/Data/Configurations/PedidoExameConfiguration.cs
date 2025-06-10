using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Data.Configurations;

public class PedidoExameConfiguration : IEntityTypeConfiguration<PedidoExame>
{
    public void Configure(EntityTypeBuilder<PedidoExame> builder)
    {
        builder.HasOne(pe => pe.Consulta)
               .WithMany(c => c.PedidosExames)
               .HasForeignKey(pe => pe.ConsultaId);
    }
}