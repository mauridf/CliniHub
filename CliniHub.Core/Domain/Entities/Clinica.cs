namespace CliniHub.Core.Domain.Entities;

public class Clinica
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string CNPJ { get; set; }
    public string EnderecoCompleto { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string? Logotipo { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }

    public ICollection<Atendente> Atendentes { get; set; }
    public ICollection<Medico> Medicos { get; set; }
    public ICollection<Agendamento> Agendamentos { get; set; }
    public ICollection<Laudo> Laudos { get; set; }
    public ICollection<DisponibilidadeMedico> DisponibilidadesMedicos { get; set; }
    public ICollection<BloqueioAgendaMedico> BloqueiosAgendaMedicos { get; set; }
}