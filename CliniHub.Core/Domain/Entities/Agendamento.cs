using CliniHub.Core.Domain.Enums;

namespace CliniHub.Core.Domain.Entities;

public class Agendamento
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public Guid ClinicaId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime DataHora { get; set; }
    public TipoAgendamento Tipo { get; set; }
    public StatusAgendamento Status { get; set; }
    public string Observacao { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }

    public Paciente Paciente { get; set; }
    public Clinica Clinica { get; set; }
    public Medico Medico { get; set; }
    public Laudo Laudo { get; set; }
    public Consulta Consulta { get; set; }
}