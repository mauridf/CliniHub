﻿using CliniHub.Core.Domain.Entities;
using CliniHub.Core.Domain.Repositories;
using CliniHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Repositories
{
    public class ClinicaRepository : IClinicaRepository
    {
        private readonly CliniHubDbContext _context;

        public ClinicaRepository(CliniHubDbContext context)
        {
            _context = context;
        }

        public async Task<Clinica> GetByIdAsync(Guid id)
        {
            return await _context.Clinicas
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Clinica>> GetAllAsync()
        {
            return await _context.Clinicas
                .ToListAsync();
        }

        public async Task AddAsync(Clinica clinica)
        {
            await _context.Clinicas.AddAsync(clinica);
        }

        public async Task UpdateAsync(Clinica clinica)
        {
            _context.Clinicas.Update(clinica);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Clinica clinica)
        {
            _context.Clinicas.Remove(clinica);
            await Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Clinica>> GetClinicasWithMedicosAsync()
        {
            return await _context.Clinicas
                .Include(c => c.Medicos)
                    .ThenInclude(m => m.Usuario)
                .Include(c => c.Medicos)
                    .ThenInclude(m => m.EspecialidadeMedica)
                .Where(c => c.Medicos.Any())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Clinica>> GetClinicasByUFAsync(string uf)
        {
            return await _context.Clinicas
                .Where(c => c.UF == uf.ToUpper())
                .AsNoTracking()
                .ToListAsync();
        }
    }
}