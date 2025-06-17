using CliniHub.Core.Domain.Entities;
using CliniHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Repositories;

public class EspecialidadeMedicaRepository : IEspecialidadeMedicaRepository
{
    private readonly CliniHubDbContext _context;

    public EspecialidadeMedicaRepository(CliniHubDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(EspecialidadeMedica especialidadeMedica)
    {
        await _context.EspecialidadesMedicas.AddAsync(especialidadeMedica);
    }

    public async Task<EspecialidadeMedica> GetByIdAsync(Guid id)
    {
        return await _context.EspecialidadesMedicas
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<EspecialidadeMedica>> GetAllAsync()
    {
        return await _context.EspecialidadesMedicas
            .OrderBy(e => e.Nome)
            .ToListAsync();
    }

    public void Remove(EspecialidadeMedica especialidadeMedica)
    {
        _context.EspecialidadesMedicas.Remove(especialidadeMedica);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Update(EspecialidadeMedica especialidadeMedica)
    {
        _context.EspecialidadesMedicas.Update(especialidadeMedica);
    }
}
