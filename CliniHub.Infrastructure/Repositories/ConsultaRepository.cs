using CliniHub.Core.Domain.Entities;
using CliniHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Repositories;

public class ConsultaRepository : IConsultaRepository
{
    private readonly CliniHubDbContext _context;

    public ConsultaRepository(CliniHubDbContext context)
    {
        _context = context;
    }

    public async Task<Consulta> GetByIdAsync(Guid id)
    {
        return await _context.Consultas
            .Include(c => c.Agendamento)
                .ThenInclude(a => a.Paciente)
            .Include(c => c.Agendamento)
                .ThenInclude(a => a.Medico)
                    .ThenInclude(m => m.Usuario)
            .Include(c => c.PedidosExames)
            .Include(c => c.Receitas)
            .Include(c => c.Atestados)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Consulta>> GetAllByClinicaAsync(Guid clinicaId, DateTime dataInicio)
    {
        return await _context.Consultas
            .Include(c => c.Agendamento)
                .ThenInclude(a => a.Paciente)
            .Include(c => c.Agendamento)
                .ThenInclude(a => a.Medico)
                    .ThenInclude(m => m.Usuario)
            .Where(c => c.Agendamento.ClinicaId == clinicaId &&
                       c.CriadoEm >= dataInicio)
            .OrderByDescending(c => c.CriadoEm)
            .ToListAsync();
    }

    public async Task<IEnumerable<Consulta>> GetByPacienteAsync(Guid clinicaId, Guid pacienteId)
    {
        return await _context.Consultas
            .Include(c => c.Agendamento)
                .ThenInclude(a => a.Paciente)
            .Include(c => c.Agendamento)
                .ThenInclude(a => a.Medico)
                    .ThenInclude(m => m.Usuario)
            .Where(c => c.Agendamento.ClinicaId == clinicaId &&
                       c.Agendamento.PacienteId == pacienteId)
            .OrderByDescending(c => c.CriadoEm)
            .ToListAsync();
    }

    public async Task<IEnumerable<Consulta>> GetByMedicoAsync(Guid clinicaId, Guid medicoId)
    {
        return await _context.Consultas
            .Include(c => c.Agendamento)
                .ThenInclude(a => a.Paciente)
            .Include(c => c.Agendamento)
                .ThenInclude(a => a.Medico)
                    .ThenInclude(m => m.Usuario)
            .Where(c => c.Agendamento.ClinicaId == clinicaId &&
                       c.Agendamento.MedicoId == medicoId)
            .OrderByDescending(c => c.CriadoEm)
            .ToListAsync();
    }

    public async Task AddAsync(Consulta consulta)
    {
        await _context.Consultas.AddAsync(consulta);
    }

    public void Update(Consulta consulta)
    {
        _context.Consultas.Update(consulta);
    }

    public void Remove(Consulta consulta)
    {
        _context.Consultas.Remove(consulta);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
