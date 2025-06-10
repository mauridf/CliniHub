using CliniHub.Application.Dtos.Auth;
using CliniHub.Application.Dtos.Clinics;

namespace CliniHub.Application.Dtos.Attendants;

public class AtendenteCreateDto
{
    public Guid UsuarioId { get; set; }
    public Guid ClinicaId { get; set; }
}

public class AtendenteResponseDto
{
    public Guid Id { get; set; }
    public UsuarioResponseDto Usuario { get; set; }
    public ClinicaSummaryDto Clinica { get; set; }
}
