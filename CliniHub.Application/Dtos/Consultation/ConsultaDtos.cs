using System.ComponentModel.DataAnnotations;
using CliniHub.Application.Dtos.Atestados;
using CliniHub.Application.Dtos.Exames;
using CliniHub.Application.Dtos.Receitas;

namespace CliniHub.Application.Dtos.Consultas;

public class ConsultaCreateDto
{
    [Required]
    public Guid AgendamentoId { get; set; }

    [Required]
    public string Anamnese { get; set; }

    public string Diagnostico { get; set; }
    public string Conduta { get; set; }
}

public class ConsultaUpdateDto
{
    public string Anamnese { get; set; }
    public string Diagnostico { get; set; }
    public string Conduta { get; set; }
}

public class ConsultaResponseDto
{
    public Guid Id { get; set; }
    public string Anamnese { get; set; }
    public string Diagnostico { get; set; }
    public string Conduta { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid AgendamentoId { get; set; }
    public IEnumerable<PedidoExameResponseDto> PedidosExames { get; set; }
    public IEnumerable<ReceitaResponseDto> Receitas { get; set; }
    public IEnumerable<AtestadoResponseDto> Atestados { get; set; }
}