using AutoMapper;
using CliniHub.Application.Dtos.Appointments;
using CliniHub.Core.Domain.Entities;
using CliniHub.Core.Domain.Enums;
using CliniHub.Infrastructure.Repositories;

namespace CliniHub.Application.Features.Appointments;

public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IMapper _mapper;

    public AgendamentoService(IAgendamentoRepository agendamentoRepository, IMapper mapper)
    {
        _agendamentoRepository = agendamentoRepository;
        _mapper = mapper;
    }

    public async Task<AgendamentoResponseDto> CreateAsync(AgendamentoCreateDto agendamentoDto, Guid usuarioId)
    {
        var agendamento = _mapper.Map<Agendamento>(agendamentoDto);
        agendamento.Status = StatusAgendamento.Agendado;
        agendamento.CriadoPor = usuarioId;
        agendamento.CriadoEm = DateTime.UtcNow;

        await _agendamentoRepository.AddAsync(agendamento);
        await _agendamentoRepository.SaveChangesAsync();

        return await GetByIdAsync(agendamento.Id);
    }

    public async Task<IEnumerable<AgendamentoResponseDto>> GetAllByClinicaAsync(Guid clinicaId)
    {
        var dataInicio = DateTime.SpecifyKind(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1), DateTimeKind.Utc);
        var agendamentos = await _agendamentoRepository.GetAllByClinicaAsync(clinicaId, dataInicio);
        return _mapper.Map<IEnumerable<AgendamentoResponseDto>>(agendamentos);
    }

    public async Task<IEnumerable<AgendamentoResponseDto>> GetConsultasByClinicaAsync(Guid clinicaId)
    {
        var dataInicio = DateTime.SpecifyKind(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1), DateTimeKind.Utc);
        var agendamentos = await _agendamentoRepository.GetByTypeAndStatusAsync(
            clinicaId, TipoAgendamento.Consulta, StatusAgendamento.Agendado, dataInicio);
        return _mapper.Map<IEnumerable<AgendamentoResponseDto>>(agendamentos);
    }

    public async Task<IEnumerable<AgendamentoResponseDto>> GetExamesByClinicaAsync(Guid clinicaId)
    {
        var dataInicio = DateTime.SpecifyKind(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1), DateTimeKind.Utc);
        var agendamentos = await _agendamentoRepository.GetByTypeAndStatusAsync(
            clinicaId, TipoAgendamento.Exame, StatusAgendamento.Agendado, dataInicio);
        return _mapper.Map<IEnumerable<AgendamentoResponseDto>>(agendamentos);
    }

    public async Task<IEnumerable<AgendamentoResponseDto>> GetAgendadosByClinicaAsync(Guid clinicaId)
    {
        var dataInicio = DateTime.SpecifyKind(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1), DateTimeKind.Utc);
        var agendamentos = await _agendamentoRepository.GetByStatusAsync(
            clinicaId, StatusAgendamento.Agendado, dataInicio);
        return _mapper.Map<IEnumerable<AgendamentoResponseDto>>(agendamentos);
    }

    public async Task<IEnumerable<AgendamentoResponseDto>> GetRealizadosByClinicaAsync(Guid clinicaId)
    {
        var dataInicio = DateTime.SpecifyKind(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1), DateTimeKind.Utc);
        var agendamentos = await _agendamentoRepository.GetByStatusAsync(
            clinicaId, StatusAgendamento.Realizado, dataInicio);
        return _mapper.Map<IEnumerable<AgendamentoResponseDto>>(agendamentos);
    }

    public async Task<IEnumerable<AgendamentoResponseDto>> GetCanceladosByClinicaAsync(Guid clinicaId)
    {
        var dataInicio = DateTime.SpecifyKind(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1), DateTimeKind.Utc);
        var agendamentos = await _agendamentoRepository.GetByStatusAsync(
            clinicaId, StatusAgendamento.Cancelado, dataInicio);
        return _mapper.Map<IEnumerable<AgendamentoResponseDto>>(agendamentos);
    }

    public async Task<AgendamentoResponseDto> GetByIdAsync(Guid id)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(id);
        return _mapper.Map<AgendamentoResponseDto>(agendamento);
    }

    public async Task<IEnumerable<AgendamentoResponseDto>> GetByPacienteAsync(Guid clinicaId, Guid pacienteId)
    {
        var agendamentos = await _agendamentoRepository.GetByPacienteAsync(clinicaId, pacienteId);
        return _mapper.Map<IEnumerable<AgendamentoResponseDto>>(agendamentos);
    }

    public async Task<AgendamentoResponseDto> UpdateAsync(Guid id, AgendamentoUpdateDto agendamentoDto, Guid usuarioId)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(id);
        if (agendamento == null) return null;

        _mapper.Map(agendamentoDto, agendamento);
        agendamento.AlteradoPor = usuarioId;
        agendamento.AlteradoEm = DateTime.UtcNow;

        _agendamentoRepository.Update(agendamento);
        await _agendamentoRepository.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<AgendamentoResponseDto> UpdateStatusAsync(Guid id, StatusAgendamento status, Guid usuarioId)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(id);
        if (agendamento == null) return null;

        agendamento.Status = status;
        agendamento.AlteradoPor = usuarioId;
        agendamento.AlteradoEm = DateTime.UtcNow;

        _agendamentoRepository.Update(agendamento);
        await _agendamentoRepository.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(id);
        if (agendamento == null) return false;

        _agendamentoRepository.Remove(agendamento);
        return await _agendamentoRepository.SaveChangesAsync();
    }
}