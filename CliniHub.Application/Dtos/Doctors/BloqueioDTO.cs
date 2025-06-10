namespace CliniHub.Application.Dtos.Doctors;

public class BloqueioCreateDto
{
    public Guid MedicoId { get; set; }
    public Guid ClinicaId { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFim { get; set; }
    public string Motivo { get; set; }
}

public class BloqueioResponseDto
{
    public Guid Id { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFim { get; set; }
    public string Motivo { get; set; }
    public MedicoSummaryDto Medico { get; set; }
}
