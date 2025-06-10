namespace CliniHub.Core.Domain.Entities;

public class Atendente
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid ClinicaId { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }

    public Usuario Usuario { get; set; }
    public Clinica Clinica { get; set; }
}