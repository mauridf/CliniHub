using CliniHub.Application.Dtos.Doctors;

namespace CliniHub.Application.Features.Doctors;

public interface IEspecialidadeMedicaService
{
    Task<EspecialidadeResponseDto> CreateAsync(EspecialidadeCreateDto especialidadeDto, Guid usuarioId);
    Task<EspecialidadeResponseDto> GetByIdAsync(Guid id);
    Task<IEnumerable<EspecialidadeResponseDto>> GetAllAsync();
    Task<EspecialidadeResponseDto> UpdateAsync(Guid id, EspecialidadeUpdateDto especialidadeDto, Guid usuarioId);
    Task<bool> DeleteAsync(Guid id);
}
