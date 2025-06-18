using CliniHub.Application.Dtos.Appointments;
using CliniHub.Core.Domain.Enums;

namespace CliniHub.Application.Features.Appointments;

public interface IAgendamentoService
{
    Task<AgendamentoResponseDto> CreateAsync(AgendamentoCreateDto agendamentoDto, Guid usuarioId);
    Task<IEnumerable<AgendamentoResponseDto>> GetAllByClinicaAsync(Guid clinicaId);
    Task<IEnumerable<AgendamentoResponseDto>> GetConsultasByClinicaAsync(Guid clinicaId);
    Task<IEnumerable<AgendamentoResponseDto>> GetExamesByClinicaAsync(Guid clinicaId);
    Task<IEnumerable<AgendamentoResponseDto>> GetAgendadosByClinicaAsync(Guid clinicaId);
    Task<IEnumerable<AgendamentoResponseDto>> GetRealizadosByClinicaAsync(Guid clinicaId);
    Task<IEnumerable<AgendamentoResponseDto>> GetCanceladosByClinicaAsync(Guid clinicaId);
    Task<AgendamentoResponseDto> GetByIdAsync(Guid id);
    Task<IEnumerable<AgendamentoResponseDto>> GetByPacienteAsync(Guid clinicaId, Guid pacienteId);
    Task<AgendamentoResponseDto> UpdateAsync(Guid id, AgendamentoUpdateDto agendamentoDto, Guid usuarioId);
    Task<AgendamentoResponseDto> UpdateStatusAsync(Guid id, StatusAgendamento status, Guid usuarioId);
    Task<bool> DeleteAsync(Guid id);
}