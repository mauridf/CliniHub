using CliniHub.Core.Domain.Entities;

namespace CliniHub.Infrastructure.Repositories;

public interface IEspecialidadeMedicaRepository
{
    Task<EspecialidadeMedica> GetByIdAsync(Guid id);
    Task<IEnumerable<EspecialidadeMedica>> GetAllAsync();
    Task AddAsync(EspecialidadeMedica especialidadeMedica);
    void Update(EspecialidadeMedica especialidadeMedica);
    void Remove(EspecialidadeMedica especialidadeMedica);
    Task<bool> SaveChangesAsync();
}
