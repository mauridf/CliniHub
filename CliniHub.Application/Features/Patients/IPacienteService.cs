using CliniHub.Application.Dtos.Patients;

namespace CliniHub.Application.Features.Patients;

public interface IPacienteService
{
    Task<PacienteResponseDto> CreateAsync(PacienteCreateDto pacienteDto, Guid usuarioId);
    Task<IEnumerable<PacienteSummaryDto>> GetAllAsync();
    Task<PacienteResponseDto> GetByIdAsync(Guid id);
    Task<IEnumerable<PacienteSummaryDto>> GetByNomeAsync(string nome);
    Task<PacienteResponseDto> GetByCpfAsync(string cpf);
    Task<PacienteResponseDto> UpdateAsync(Guid id, PacienteUpdateDto pacienteDto, Guid usuarioId);
    Task<bool> DeleteAsync(Guid id);
}