namespace CliniHub.Core.Domain.Entities;

public class EspecialidadeMedica
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Observacao { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }

    public ICollection<Medico> Medicos { get; set; }
}