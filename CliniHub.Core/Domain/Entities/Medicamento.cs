namespace CliniHub.Core.Domain.Entities;

public class Medicamento
{
    public Guid Id { get; set; }
    public string NomeComercial { get; set; }
    public string PrincipioAtivo { get; set; }
    public string Apresentacao { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }

    public ICollection<ItemReceita> ItensReceitas { get; set; }
}