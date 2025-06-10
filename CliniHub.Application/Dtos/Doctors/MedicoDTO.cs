using CliniHub.Application.Dtos.Auth;

namespace CliniHub.Application.Dtos.Doctors;

public class MedicoCreateDto
{
    public Guid UsuarioId { get; set; }
    public Guid ClinicaId { get; set; }
    public string CRM { get; set; }
    public Guid EspecialidadeMedicaId { get; set; }
}

public class MedicoUpdateDto
{
    public string CRM { get; set; }
    public Guid EspecialidadeMedicaId { get; set; }
    public bool Ativo { get; set; }
}

public class MedicoSummaryDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string CRM { get; set; }
    public string Especialidade { get; set; }
}

public class MedicoResponseDto
{
    public Guid Id { get; set; }
    public string CRM { get; set; }
    public bool Ativo { get; set; }

    public UsuarioResponseDto Usuario { get; set; }
    public EspecialidadeResponseDto Especialidade { get; set; }

    public ICollection<DisponibilidadeResponseDto> Disponibilidades { get; set; }
}
