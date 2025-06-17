using AutoMapper;
using CliniHub.Application.Dtos.Attendants;
using CliniHub.Core.Domain.Entities;
using CliniHub.Core.Domain.Repositories;

namespace CliniHub.Application.Features.Attendants;

public class AtendenteService : IAtendenteService
{
    private readonly IAtendenteRepository _atendenteRepository;
    private readonly IMapper _mapper;

    public AtendenteService(IAtendenteRepository atendenteRepository, IMapper mapper)
    {
        _atendenteRepository = atendenteRepository;
        _mapper = mapper;
    }

    public async Task<AtendenteResponseDto> GetByIdAsync(Guid id)
    {
        var atendente = await _atendenteRepository.GetByIdAsync(id);
        return _mapper.Map<AtendenteResponseDto>(atendente);
    }

    public async Task<IEnumerable<AtendenteResponseDto>> GetByClinicaAsync(Guid clinicaId)
    {
        var atendentes = await _atendenteRepository.GetByClinicaAsync(clinicaId);
        return _mapper.Map<IEnumerable<AtendenteResponseDto>>(atendentes);
    }

    public async Task<AtendenteResponseDto> CreateAsync(AtendenteCreateDto atendenteDto, Guid usuarioId)
    {
        var atendente = _mapper.Map<Atendente>(atendenteDto);
        atendente.CriadoPor = usuarioId;
        atendente.CriadoEm = DateTime.UtcNow;

        await _atendenteRepository.AddAsync(atendente);
        await _atendenteRepository.SaveChangesAsync();

        return await GetByIdAsync(atendente.Id);
    }

    public async Task<AtendenteResponseDto> UpdateAsync(Guid id, AtendenteCreateDto atendenteDto)
    {
        var atendente = await _atendenteRepository.GetByIdAsync(id);
        if (atendente == null) return null;

        _mapper.Map(atendenteDto, atendente);
        atendente.AlteradoEm = DateTime.UtcNow;

        await _atendenteRepository.UpdateAsync(atendente);
        await _atendenteRepository.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var atendente = await _atendenteRepository.GetByIdAsync(id);
        if (atendente == null) return false;

        await _atendenteRepository.DeleteAsync(atendente);
        return await _atendenteRepository.SaveChangesAsync();
    }
}