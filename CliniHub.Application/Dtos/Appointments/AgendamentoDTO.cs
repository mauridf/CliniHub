using CliniHub.Application.Dtos.Clinics;
using CliniHub.Application.Dtos.Doctors;
using CliniHub.Application.Dtos.Patients;
using CliniHub.Core.Domain.Enums;

namespace CliniHub.Application.Dtos.Appointments;

public class AgendamentoCreateDto
{
    public Guid PacienteId { get; set; }
    public Guid ClinicaId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime DataHora { get; set; }
    public TipoAgendamento Tipo { get; set; }
    public string Observacao { get; set; }
}

public class AgendamentoUpdateDto
{
    public DateTime? DataHora { get; set; }
    public TipoAgendamento? Tipo { get; set; }
    public StatusAgendamento? Status { get; set; }
    public string Observacao { get; set; }
}

public class AgendamentoCalendarDto
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Title { get; set; }
    public TipoAgendamento Tipo { get; set; }
    public StatusAgendamento Status { get; set; }
    public string PacienteNome { get; set; }
    public string MedicoNome { get; set; }
}

public class AgendamentoResponseDto
{
    public Guid Id { get; set; }
    public DateTime DataHora { get; set; }
    public TipoAgendamento Tipo { get; set; }
    public StatusAgendamento Status { get; set; }
    public string Observacao { get; set; }

    public PacienteSummaryDto Paciente { get; set; }
    public MedicoSummaryDto Medico { get; set; }
    public ClinicaSummaryDto Clinica { get; set; }
}
