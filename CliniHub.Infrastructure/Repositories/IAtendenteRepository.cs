using CliniHub.Core.Domain.Entities;

namespace CliniHub.Core.Domain.Repositories;

public interface IAtendenteRepository
{
    Task<Atendente> GetByIdAsync(Guid id);
    Task<IEnumerable<Atendente>> GetByClinicaAsync(Guid clinicaId);
    Task AddAsync(Atendente atendente);
    Task UpdateAsync(Atendente atendente);
    Task DeleteAsync(Atendente atendente);
    Task<bool> SaveChangesAsync();
}