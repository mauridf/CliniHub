using CliniHub.Core.Domain.Enums;

namespace CliniHub.Application.Dtos.Patients;

public class PacienteCreateDto
{
    public string Nome { get; set; }
    public string EnderecoCompleto { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public Genero? Genero { get; set; }
}

public class PacienteUpdateDto
{
    public string Nome { get; set; }
    public string EnderecoCompleto { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public Genero? Genero { get; set; }
}

public class PacienteResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string EnderecoCompleto { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public Genero? Genero { get; set; }
}

public class PacienteSummaryDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
}
