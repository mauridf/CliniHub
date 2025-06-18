using CliniHub.Application.Dtos.Consultas;

namespace CliniHub.Application.Features.Consultation;

public interface IConsultaService
{
    Task<ConsultaResponseDto> CreateAsync(ConsultaCreateDto consultaDto, Guid usuarioId);
    Task<IEnumerable<ConsultaResponseDto>> GetAllByClinicaAsync(Guid clinicaId);
    Task<ConsultaResponseDto> GetByIdAsync(Guid id);
    Task<IEnumerable<ConsultaResponseDto>> GetByPacienteAsync(Guid clinicaId, Guid pacienteId);
    Task<IEnumerable<ConsultaResponseDto>> GetByMedicoAsync(Guid clinicaId, Guid medicoId);
    Task<ConsultaResponseDto> UpdateAsync(Guid id, ConsultaUpdateDto consultaDto, Guid usuarioId);
    Task<bool> DeleteAsync(Guid id);
}
