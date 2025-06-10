using CliniHub.Core.Domain.Enums;

namespace CliniHub.Core.Domain.Entities;

public class DisponibilidadeMedico
{
    public Guid Id { get; set; }
    public Guid MedicoId { get; set; }
    public Guid ClinicaId { get; set; }
    public DiaSemana DiaSemana { get; set; }
    public TimeSpan HorarioInicio { get; set; }
    public TimeSpan HorarioFim { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }

    public Medico Medico { get; set; }
    public Clinica Clinica { get; set; }
}