using CliniHub.Core.Domain.Entities;
using CliniHub.Core.Domain.Repositories;
using CliniHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Repositories;

public class AtendenteRepository : IAtendenteRepository
{
    private readonly CliniHubDbContext _context;

    public AtendenteRepository(CliniHubDbContext context)
    {
        _context = context;
    }

    public async Task<Atendente> GetByIdAsync(Guid id)
    {
        return await _context.Atendentes
            .Include(a => a.Usuario)
            .Include(a => a.Clinica)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Atendente>> GetByClinicaAsync(Guid clinicaId)
    {
        return await _context.Atendentes
            .Include(a => a.Usuario)
            .Include(a => a.Clinica)
            .Where(a => a.ClinicaId == clinicaId)
            .ToListAsync();
    }

    public async Task AddAsync(Atendente atendente)
    {
        await _context.Atendentes.AddAsync(atendente);
    }

    public async Task UpdateAsync(Atendente atendente)
    {
        _context.Atendentes.Update(atendente);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Atendente atendente)
    {
        _context.Atendentes.Remove(atendente);
        await Task.CompletedTask;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}