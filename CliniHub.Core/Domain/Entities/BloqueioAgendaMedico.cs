namespace CliniHub.Core.Domain.Entities;

public class BloqueioAgendaMedico
{
    public Guid Id { get; set; }
    public Guid MedicoId { get; set; }
    public Guid ClinicaId { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFim { get; set; }
    public string Motivo { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }

    public Medico Medico { get; set; }
    public Clinica Clinica { get; set; }
}