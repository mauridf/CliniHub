using CliniHub.Core.Domain.Entities;

namespace CliniHub.Infrastructure.Repositories;

public interface IPacienteRepository
{
    Task<Paciente> GetByIdAsync(Guid id);
    Task<Paciente> GetByCpfAsync(string cpf);
    Task<IEnumerable<Paciente>> GetAllAsync();
    Task<IEnumerable<Paciente>> GetByNomeAsync(string nome);
    Task AddAsync(Paciente paciente);
    void Update(Paciente paciente);
    void Remove(Paciente paciente);
    Task<bool> SaveChangesAsync();
}