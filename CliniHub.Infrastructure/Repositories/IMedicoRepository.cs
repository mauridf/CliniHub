using CliniHub.Core.Domain.Entities;

namespace CliniHub.Infrastructure.Repositories;

public interface IMedicoRepository
{
    Task<Medico> GetByIdAsync(Guid id);
    Task<Medico> GetByCrmAsync(string crm);
    Task<IEnumerable<Medico>> GetAllByClinicaAsync(Guid clinicaId);
    Task<IEnumerable<Medico>> GetByEspecialidadeAndClinicaAsync(Guid clinicaId, Guid especialidadeId);
    Task AddAsync(Medico medico);
    void Update(Medico medico);
    void Remove(Medico medico);
    Task<bool> SaveChangesAsync();
}
