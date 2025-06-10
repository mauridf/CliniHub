namespace CliniHub.Core.Domain.Entities;

public class TipoExame
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool RequerPreparo { get; set; }
    public string Preparo { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }

    public ICollection<PedidoExame> PedidosExames { get; set; }
}
