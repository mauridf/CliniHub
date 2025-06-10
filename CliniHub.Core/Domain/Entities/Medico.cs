namespace CliniHub.Core.Domain.Entities;

public class Medico
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid ClinicaId { get; set; }
    public string CRM { get; set; }
    public Guid EspecialidadeMedicaId { get; set; }
    public bool Ativo { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }

    public Usuario Usuario { get; set; }
    public Clinica Clinica { get; set; }
    public EspecialidadeMedica EspecialidadeMedica { get; set; }
    public ICollection<Agendamento> Agendamentos { get; set; }
    public ICollection<Laudo> Laudos { get; set; }
    public ICollection<DisponibilidadeMedico> Disponibilidades { get; set; }
    public ICollection<BloqueioAgendaMedico> BloqueiosAgenda { get; set; }
}