using CliniHub.Core.Domain.Enums;

namespace CliniHub.Application.Dtos.Doctors;

public class DisponibilidadeCreateDto
{
    public Guid MedicoId { get; set; }
    public Guid ClinicaId { get; set; }
    public DiaSemana DiaSemana { get; set; }
    public TimeSpan HorarioInicio { get; set; }
    public TimeSpan HorarioFim { get; set; }
}

public class DisponibilidadeResponseDto
{
    public Guid Id { get; set; }
    public DiaSemana DiaSemana { get; set; }
    public string HorarioInicio { get; set; }
    public string HorarioFim { get; set; }
}