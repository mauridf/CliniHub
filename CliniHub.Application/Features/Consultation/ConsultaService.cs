using AutoMapper;
using CliniHub.Application.Dtos.Consultas;
using CliniHub.Core.Domain.Entities;
using CliniHub.Infrastructure.Repositories;

namespace CliniHub.Application.Features.Consultation;

public class ConsultaService : IConsultaService
{
    private readonly IConsultaRepository _consultaRepository;
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IMapper _mapper;

    public ConsultaService(
        IConsultaRepository consultaRepository,
        IAgendamentoRepository agendamentoRepository,
        IMapper mapper)
    {
        _consultaRepository = consultaRepository;
        _agendamentoRepository = agendamentoRepository;
        _mapper = mapper;
    }

    public async Task<ConsultaResponseDto> CreateAsync(ConsultaCreateDto consultaDto, Guid usuarioId)
    {
        // Verificar se o agendamento existe e pertence à clínica
        var agendamento = await _agendamentoRepository.GetByIdAsync(consultaDto.AgendamentoId);
        if (agendamento == null)
        {
            throw new ArgumentException("Agendamento não encontrado");
        }

        var consulta = _mapper.Map<Consulta>(consultaDto);
        consulta.CriadoPor = usuarioId;
        consulta.CriadoEm = DateTime.UtcNow;

        await _consultaRepository.AddAsync(consulta);
        await _consultaRepository.SaveChangesAsync();

        return await GetByIdAsync(consulta.Id);
    }

    public async Task<IEnumerable<ConsultaResponseDto>> GetAllByClinicaAsync(Guid clinicaId)
    {
        var dataInicio = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        var consultas = await _consultaRepository.GetAllByClinicaAsync(clinicaId, dataInicio);
        return _mapper.Map<IEnumerable<ConsultaResponseDto>>(consultas);
    }

    public async Task<ConsultaResponseDto> GetByIdAsync(Guid id)
    {
        var consulta = await _consultaRepository.GetByIdAsync(id);
        if (consulta == null) return null;

        var result = _mapper.Map<ConsultaResponseDto>(consulta);
        return result;
    }

    public async Task<IEnumerable<ConsultaResponseDto>> GetByPacienteAsync(Guid clinicaId, Guid pacienteId)
    {
        var consultas = await _consultaRepository.GetByPacienteAsync(clinicaId, pacienteId);
        return _mapper.Map<IEnumerable<ConsultaResponseDto>>(consultas);
    }

    public async Task<IEnumerable<ConsultaResponseDto>> GetByMedicoAsync(Guid clinicaId, Guid medicoId)
    {
        var consultas = await _consultaRepository.GetByMedicoAsync(clinicaId, medicoId);
        return _mapper.Map<IEnumerable<ConsultaResponseDto>>(consultas);
    }

    public async Task<ConsultaResponseDto> UpdateAsync(Guid id, ConsultaUpdateDto consultaDto, Guid usuarioId)
    {
        var consulta = await _consultaRepository.GetByIdAsync(id);
        if (consulta == null) return null;

        _mapper.Map(consultaDto, consulta);
        consulta.AlteradoPor = usuarioId;
        consulta.AlteradoEm = DateTime.UtcNow;

        _consultaRepository.Update(consulta);
        await _consultaRepository.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var consulta = await _consultaRepository.GetByIdAsync(id);
        if (consulta == null) return false;

        _consultaRepository.Remove(consulta);
        return await _consultaRepository.SaveChangesAsync();
    }
}
