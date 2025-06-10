using System.ComponentModel.DataAnnotations;

namespace CliniHub.Application.Dtos.Atestados;

public class AtestadoCreateDto
{
    [Required]
    public Guid ConsultaId { get; set; }

    [Required]
    public string Conteudo { get; set; }

    public int DiasRepouso { get; set; }

    [Required]
    public DateTime Validade { get; set; }
}

public class AtestadoResponseDto
{
    public Guid Id { get; set; }
    public string Conteudo { get; set; }
    public int DiasRepouso { get; set; }
    public DateTime Validade { get; set; }
    public DateTime CriadoEm { get; set; }
}