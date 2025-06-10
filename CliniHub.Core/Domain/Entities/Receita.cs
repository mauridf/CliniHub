namespace CliniHub.Core.Domain.Entities;

public class Receita
{
    public Guid Id { get; set; }
    public Guid ConsultaId { get; set; }
    public string Conteudo { get; set; }
    public DateTime Validade { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }

    public Consulta Consulta { get; set; }
    public ICollection<ItemReceita> Itens { get; set; }
}