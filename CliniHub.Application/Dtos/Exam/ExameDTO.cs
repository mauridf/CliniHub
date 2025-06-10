using System.ComponentModel.DataAnnotations;

namespace CliniHub.Application.Dtos.Exames;

public class TipoExameDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool RequerPreparo { get; set; }
    public string Preparo { get; set; }
}

public class PedidoExameDto
{
    [Required]
    public Guid ConsultaId { get; set; }

    [Required]
    public Guid TipoExameId { get; set; }

    public string Observacoes { get; set; }
}

public class PedidoExameResponseDto
{
    public Guid Id { get; set; }
    public Guid TipoExameId { get; set; }
    public string TipoExameNome { get; set; }
    public string Observacoes { get; set; }
    public DateTime? DataRealizacao { get; set; }
}