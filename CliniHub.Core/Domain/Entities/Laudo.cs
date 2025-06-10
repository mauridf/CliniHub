namespace CliniHub.Core.Domain.Entities;

public class Laudo
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public Guid ClinicaId { get; set; }
    public Guid MedicoId { get; set; }
    public Guid? AgendamentoId { get; set; }
    public string ConteudoLaudo { get; set; }
    public string LaudoPDF { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }

    public Paciente Paciente { get; set; }
    public Clinica Clinica { get; set; }
    public Medico Medico { get; set; }
    public Agendamento Agendamento { get; set; }
}