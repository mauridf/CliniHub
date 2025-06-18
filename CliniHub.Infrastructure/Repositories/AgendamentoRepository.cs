using CliniHub.Core.Domain.Entities;
using CliniHub.Core.Domain.Enums;
using CliniHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Repositories;

public class AgendamentoRepository : IAgendamentoRepository
{
    private readonly CliniHubDbContext _context;

    public AgendamentoRepository(CliniHubDbContext context)
    {
        _context = context;
    }

    public async Task<Agendamento> GetByIdAsync(Guid id)
    {
        return await _context.Agendamentos
            .Include(a => a.Paciente)
            .Include(a => a.Medico)
                .ThenInclude(m => m.Usuario)
            .Include(a => a.Clinica)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Agendamento>> GetAllByClinicaAsync(Guid clinicaId, DateTime dataInicio)
    {
        // Garantir que a data está em UTC
        var dataInicioUtc = dataInicio.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(dataInicio, DateTimeKind.Utc)
            : dataInicio.ToUniversalTime();

        return await _context.Agendamentos
            .Include(a => a.Paciente)
            .Include(a => a.Medico)
                .ThenInclude(m => m.Usuario)
            .Where(a => a.ClinicaId == clinicaId && a.DataHora >= dataInicioUtc)
            .OrderBy(a => a.DataHora)
            .ToListAsync();
    }

    public async Task<IEnumerable<Agendamento>> GetByTypeAndStatusAsync(Guid clinicaId, TipoAgendamento tipo, StatusAgendamento status, DateTime dataInicio)
    {
        // Garantir que a data está em UTC
        var dataInicioUtc = dataInicio.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(dataInicio, DateTimeKind.Utc)
            : dataInicio.ToUniversalTime();

        return await _context.Agendamentos
            .Include(a => a.Paciente)
            .Include(a => a.Medico)
                .ThenInclude(m => m.Usuario)
            .Where(a => a.ClinicaId == clinicaId &&
                       a.Tipo == tipo &&
                       a.Status == status &&
                       a.DataHora >= dataInicioUtc)
            .OrderBy(a => a.DataHora)
            .ToListAsync();
    }

    public async Task<IEnumerable<Agendamento>> GetByStatusAsync(Guid clinicaId, StatusAgendamento status, DateTime dataInicio)
    {
        // Garantir que a data está em UTC
        var dataInicioUtc = dataInicio.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(dataInicio, DateTimeKind.Utc)
            : dataInicio.ToUniversalTime();

        return await _context.Agendamentos
            .Include(a => a.Paciente)
            .Include(a => a.Medico)
                .ThenInclude(m => m.Usuario)
            .Where(a => a.ClinicaId == clinicaId &&
                       a.Status == status &&
                       a.DataHora >= dataInicioUtc)
            .OrderBy(a => a.DataHora)
            .ToListAsync();
    }

    public async Task<IEnumerable<Agendamento>> GetByPacienteAsync(Guid clinicaId, Guid pacienteId)
    {
        return await _context.Agendamentos
            .Include(a => a.Paciente)
            .Include(a => a.Medico)
                .ThenInclude(m => m.Usuario)
            .Where(a => a.ClinicaId == clinicaId && a.PacienteId == pacienteId)
            .OrderByDescending(a => a.DataHora)
            .ToListAsync();
    }

    public async Task AddAsync(Agendamento agendamento)
    {
        await _context.Agendamentos.AddAsync(agendamento);
    }

    public void Update(Agendamento agendamento)
    {
        _context.Agendamentos.Update(agendamento);
    }

    public void Remove(Agendamento agendamento)
    {
        _context.Agendamentos.Remove(agendamento);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
