using CliniHub.Application.Dtos.Attendants;

namespace CliniHub.Application.Features.Attendants;

public interface IAtendenteService
{
    Task<AtendenteResponseDto> GetByIdAsync(Guid id);
    Task<IEnumerable<AtendenteResponseDto>> GetByClinicaAsync(Guid clinicaId);
    Task<AtendenteResponseDto> CreateAsync(AtendenteCreateDto atendenteDto, Guid usuarioId);
    Task<AtendenteResponseDto> UpdateAsync(Guid id, AtendenteCreateDto atendenteDto);
    Task<bool> DeleteAsync(Guid id);
}