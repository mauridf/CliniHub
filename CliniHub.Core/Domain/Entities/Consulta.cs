namespace CliniHub.Core.Domain.Entities;

public class Consulta
{
    public Guid Id { get; set; }
    public Guid AgendamentoId { get; set; }
    public string Anamnese { get; set; } // Histórico do paciente
    public string Diagnostico { get; set; }
    public string Conduta { get; set; } // Tratamento proposto
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }

    public Agendamento Agendamento { get; set; }
    public ICollection<PedidoExame> PedidosExames { get; set; }
    public ICollection<Receita> Receitas { get; set; }
    public ICollection<Atestado> Atestados { get; set; }
}