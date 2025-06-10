using CliniHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Data.Configurations;

public class ItemReceitaConfiguration : IEntityTypeConfiguration<ItemReceita>
{
    public void Configure(EntityTypeBuilder<ItemReceita> builder)
    {
        builder.ToTable("ItensReceitas");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Posologia)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Observacao)
            .HasMaxLength(200);

        builder.HasOne(i => i.Receita)
            .WithMany(r => r.Itens)
            .HasForeignKey(i => i.ReceitaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Medicamento)
            .WithMany(m => m.ItensReceitas)
            .HasForeignKey(i => i.MedicamentoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
