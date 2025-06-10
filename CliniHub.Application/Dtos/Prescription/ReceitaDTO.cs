using System.ComponentModel.DataAnnotations;

namespace CliniHub.Application.Dtos.Receitas;

public class ReceitaCreateDto
{
    [Required]
    public Guid ConsultaId { get; set; }

    [Required]
    public DateTime Validade { get; set; }

    public string Conteudo { get; set; }
    public IEnumerable<ItemReceitaDto> Itens { get; set; }
}

public class ReceitaResponseDto
{
    public Guid Id { get; set; }
    public string Conteudo { get; set; }
    public DateTime Validade { get; set; }
    public DateTime CriadoEm { get; set; }
    public IEnumerable<ItemReceitaResponseDto> Itens { get; set; }
}

public class ItemReceitaDto
{
    [Required]
    public Guid MedicamentoId { get; set; }

    [Required]
    public string Posologia { get; set; }

    public string Observacao { get; set; }
}

public class ItemReceitaResponseDto
{
    public Guid MedicamentoId { get; set; }
    public string MedicamentoNome { get; set; }
    public string Posologia { get; set; }
    public string Observacao { get; set; }
}