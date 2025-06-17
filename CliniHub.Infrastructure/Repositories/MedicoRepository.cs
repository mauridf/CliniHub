using CliniHub.Core.Domain.Entities;
using CliniHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Repositories;

public class MedicoRepository : IMedicoRepository
{
    private readonly CliniHubDbContext _context;

    public MedicoRepository(CliniHubDbContext context)
    {
        _context = context;
    }

    public async Task<Medico> GetByIdAsync(Guid id)
    {
        return await _context.Medicos
            .Include(m => m.Usuario)
            .Include(m => m.EspecialidadeMedica)
            .Include(m => m.Disponibilidades)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Medico> GetByCrmAsync(string crm)
    {
        return await _context.Medicos
            .Include(m => m.Usuario)
            .Include(m => m.EspecialidadeMedica)
            .FirstOrDefaultAsync(m => m.CRM == crm);
    }

    public async Task<IEnumerable<Medico>> GetAllByClinicaAsync(Guid clinicaId)
    {
        return await _context.Medicos
            .Include(m => m.Usuario)
            .Include(m => m.EspecialidadeMedica)
            .Where(m => m.ClinicaId == clinicaId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Medico>> GetByEspecialidadeAndClinicaAsync(Guid clinicaId, Guid especialidadeId)
    {
        return await _context.Medicos
            .Include(m => m.Usuario)
            .Include(m => m.EspecialidadeMedica)
            .Where(m => m.ClinicaId == clinicaId && m.EspecialidadeMedicaId == especialidadeId)
            .OrderBy(m => m.Usuario.Nome)
            .ToListAsync();
    }

    public async Task AddAsync(Medico medico)
    {
        await _context.Medicos.AddAsync(medico);
    }

    public void Update(Medico medico)
    {
        _context.Medicos.Update(medico);
    }

    public void Remove(Medico medico)
    {
        _context.Medicos.Remove(medico);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}