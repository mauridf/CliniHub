using CliniHub.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CliniHub.Core.Domain.Repositories;

public interface IClinicaRepository
{
    Task<Clinica> GetByIdAsync(Guid id);
    Task<IEnumerable<Clinica>> GetAllAsync();
    Task AddAsync(Clinica clinica);
    Task UpdateAsync(Clinica clinica);
    Task DeleteAsync(Clinica clinica);
    Task<bool> SaveChangesAsync();
}