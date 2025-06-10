namespace CliniHub.Core.Domain.Entities;

public class ItemReceita
{
    public Guid Id { get; set; }
    public Guid ReceitaId { get; set; }
    public Guid MedicamentoId { get; set; }
    public string Posologia { get; set; }
    public string Observacao { get; set; }

    public Receita Receita { get; set; }
    public Medicamento Medicamento { get; set; }
}