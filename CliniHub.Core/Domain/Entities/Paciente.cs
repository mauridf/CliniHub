using CliniHub.Core.Domain.Enums;

namespace CliniHub.Core.Domain.Entities;

public class Paciente
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string EnderecoCompleto { get; set; }
    public string CPF { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public Genero? Genero { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }

    public ICollection<Agendamento> Agendamentos { get; set; }
    public ICollection<Laudo> Laudos { get; set; }
}