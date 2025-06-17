using AutoMapper;
using CliniHub.Application.Dtos.Patients;
using CliniHub.Core.Domain.Entities;
using CliniHub.Infrastructure.Repositories;

namespace CliniHub.Application.Features.Patients;

public class PacienteService : IPacienteService
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IMapper _mapper;

    public PacienteService(IPacienteRepository pacienteRepository, IMapper mapper)
    {
        _pacienteRepository = pacienteRepository;
        _mapper = mapper;
    }

    public async Task<PacienteResponseDto> CreateAsync(PacienteCreateDto pacienteDto, Guid usuarioId)
    {
        var paciente = _mapper.Map<Paciente>(pacienteDto);

        // Converter DataNascimento para UTC (assumindo que está em horário local)
        paciente.DataNascimento = DateTime.SpecifyKind(paciente.DataNascimento, DateTimeKind.Utc);

        paciente.CriadoPor = usuarioId;
        paciente.CriadoEm = DateTime.UtcNow;

        await _pacienteRepository.AddAsync(paciente);
        await _pacienteRepository.SaveChangesAsync();

        return await GetByIdAsync(paciente.Id);
    }

    public async Task<IEnumerable<PacienteSummaryDto>> GetAllAsync()
    {
        var pacientes = await _pacienteRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PacienteSummaryDto>>(pacientes);
    }

    public async Task<PacienteResponseDto> GetByIdAsync(Guid id)
    {
        var paciente = await _pacienteRepository.GetByIdAsync(id);
        return _mapper.Map<PacienteResponseDto>(paciente);
    }

    public async Task<IEnumerable<PacienteSummaryDto>> GetByNomeAsync(string nome)
    {
        var pacientes = await _pacienteRepository.GetByNomeAsync(nome);
        return _mapper.Map<IEnumerable<PacienteSummaryDto>>(pacientes);
    }

    public async Task<PacienteResponseDto> GetByCpfAsync(string cpf)
    {
        var paciente = await _pacienteRepository.GetByCpfAsync(cpf);
        return _mapper.Map<PacienteResponseDto>(paciente);
    }

    public async Task<PacienteResponseDto> UpdateAsync(Guid id, PacienteUpdateDto pacienteDto, Guid usuarioId)
    {
        var paciente = await _pacienteRepository.GetByIdAsync(id);
        if (paciente == null) return null;

        _mapper.Map(pacienteDto, paciente);
        paciente.AlteradoPor = usuarioId;
        paciente.AlteradoEm = DateTime.UtcNow;

        _pacienteRepository.Update(paciente);
        await _pacienteRepository.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var paciente = await _pacienteRepository.GetByIdAsync(id);
        if (paciente == null) return false;

        _pacienteRepository.Remove(paciente);
        return await _pacienteRepository.SaveChangesAsync();
    }
}