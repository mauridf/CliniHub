namespace CliniHub.Core.Domain.Entities;

public class PedidoExame
{
    public Guid Id { get; set; }
    public Guid ConsultaId { get; set; }
    public Guid TipoExameId { get; set; }
    public string Observacoes { get; set; }
    public DateTime? DataRealizacao { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }

    public Consulta Consulta { get; set; }
    public TipoExame TipoExame { get; set; }
    public Laudo Laudo { get; set; }
}