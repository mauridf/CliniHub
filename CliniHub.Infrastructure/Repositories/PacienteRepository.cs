using CliniHub.Core.Domain.Entities;
using CliniHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CliniHub.Infrastructure.Repositories;

public class PacienteRepository : IPacienteRepository
{
    private readonly CliniHubDbContext _context;

    public PacienteRepository(CliniHubDbContext context)
    {
        _context = context;
    }

    public async Task<Paciente> GetByIdAsync(Guid id)
    {
        return await _context.Pacientes
            .Include(p => p.Agendamentos)
            .Include(p => p.Laudos)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Paciente> GetByCpfAsync(string cpf)
    {
        return await _context.Pacientes
            .FirstOrDefaultAsync(p => p.CPF == cpf);
    }

    public async Task<IEnumerable<Paciente>> GetAllAsync()
    {
        return await _context.Pacientes
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Paciente>> GetByNomeAsync(string nome)
    {
        return await _context.Pacientes
            .Where(p => p.Nome.Contains(nome))
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task AddAsync(Paciente paciente)
    {
        await _context.Pacientes.AddAsync(paciente);
    }

    public void Update(Paciente paciente)
    {
        _context.Pacientes.Update(paciente);
    }

    public void Remove(Paciente paciente)
    {
        _context.Pacientes.Remove(paciente);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
