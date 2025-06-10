using CliniHub.Application.Dtos.Patients;

namespace CliniHub.Application.Dtos.Doctors;

public class LaudoCreateDto
{
    public Guid PacienteId { get; set; }
    public Guid ClinicaId { get; set; }
    public Guid MedicoId { get; set; }
    public Guid? AgendamentoId { get; set; }
    public string ConteudoLaudo { get; set; }
}

public class LaudoResponseDto
{
    public Guid Id { get; set; }
    public string ConteudoLaudo { get; set; }
    public string LaudoPDF { get; set; }
    public PacienteSummaryDto Paciente { get; set; }
    public MedicoSummaryDto Medico { get; set; }
    public DateTime CriadoEm { get; set; }

    // Propriedades de navegação como objetos
    public PacienteSummaryDto Pacientes { get; set; }
    public MedicoSummaryDto Medicos { get; set; }

    // Propriedades adicionais para nomes diretos (opcional)
    public string MedicoNome => Medico?.Nome;
    public string PacienteNome => Paciente?.Nome;
}

public class LaudoPdfDto
{
    public byte[] Content { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; } = "application/pdf";
}
