namespace CliniHub.Application.Dtos.Medicamentos;

public class MedicamentoDto
{
    public Guid Id { get; set; }
    public string NomeComercial { get; set; }
    public string PrincipioAtivo { get; set; }
    public string Apresentacao { get; set; }
}