using CliniHub.Application.Dtos.Doctors;

namespace CliniHub.Application.Features.Doctors;

public interface IMedicoService
{
    Task<MedicoResponseDto> CreateAsync(MedicoCreateDto medicoDto, Guid usuarioId);
    Task<IEnumerable<MedicoSummaryDto>> GetAllByClinicaAsync(Guid clinicaId);
    Task<IEnumerable<MedicoSummaryDto>> GetByEspecialidadeAndClinicaAsync(Guid clinicaId, Guid especialidadeId);
    Task<MedicoResponseDto> GetByIdAsync(Guid id);
    Task<MedicoResponseDto> GetByCrmAsync(string crm);
    Task<MedicoResponseDto> UpdateAsync(Guid id, MedicoUpdateDto medicoDto, Guid usuarioId);
    Task<bool> DeleteAsync(Guid id);
}