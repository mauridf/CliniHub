using CliniHub.Core.Domain.Entities;

namespace CliniHub.Infrastructure.Repositories;

public interface IConsultaRepository
{
    Task<Consulta> GetByIdAsync(Guid id);
    Task<IEnumerable<Consulta>> GetAllByClinicaAsync(Guid clinicaId, DateTime dataInicio);
    Task<IEnumerable<Consulta>> GetByPacienteAsync(Guid clinicaId, Guid pacienteId);
    Task<IEnumerable<Consulta>> GetByMedicoAsync(Guid clinicaId, Guid medicoId);
    Task AddAsync(Consulta consulta);
    void Update(Consulta consulta);
    void Remove(Consulta consulta);
    Task<bool> SaveChangesAsync();
}