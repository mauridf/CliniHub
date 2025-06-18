using CliniHub.Core.Domain.Entities;
using CliniHub.Core.Domain.Enums;

namespace CliniHub.Infrastructure.Repositories;

public interface IAgendamentoRepository
{
    Task<Agendamento> GetByIdAsync(Guid id);
    Task<IEnumerable<Agendamento>> GetAllByClinicaAsync(Guid clinicaId, DateTime dataInicio);
    Task<IEnumerable<Agendamento>> GetByTypeAndStatusAsync(Guid clinicaId, TipoAgendamento tipo, StatusAgendamento status, DateTime dataInicio);
    Task<IEnumerable<Agendamento>> GetByStatusAsync(Guid clinicaId, StatusAgendamento status, DateTime dataInicio);
    Task<IEnumerable<Agendamento>> GetByPacienteAsync(Guid clinicaId, Guid pacienteId);
    Task AddAsync(Agendamento agendamento);
    void Update(Agendamento agendamento);
    void Remove(Agendamento agendamento);
    Task<bool> SaveChangesAsync();
}