using CliniHub.Application.Dtos.Clinics;

namespace CliniHub.Application.Features.Clinics;

public interface IClinicaService
{
    Task<ClinicaResponseDto> GetByIdAsync(Guid id);
    Task<IEnumerable<ClinicaSummaryDto>> GetAllAsync();
    Task<ClinicaResponseDto> CreateAsync(ClinicaCreateDto clinicaDto, Guid usuarioId);
    Task<ClinicaResponseDto> UpdateAsync(Guid id, ClinicaUpdateDto clinicaDto);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateLogoAsync(Guid id, string logoPath);
    Task<IEnumerable<ClinicaMedicosResponseDto>> GetClinicasWithMedicosAsync();
    Task<IEnumerable<ClinicaSummaryDto>> GetClinicasByUFAsync(string uf);
}
