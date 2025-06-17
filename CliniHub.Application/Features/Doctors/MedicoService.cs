using AutoMapper;
using CliniHub.Application.Dtos.Doctors;
using CliniHub.Core.Domain.Entities;
using CliniHub.Infrastructure.Repositories;

namespace CliniHub.Application.Features.Doctors;

public class MedicoService : IMedicoService
{
    private readonly IMedicoRepository _medicoRepository;
    private readonly IMapper _mapper;

    public MedicoService(IMedicoRepository medicoRepository, IMapper mapper)
    {
        _medicoRepository = medicoRepository;
        _mapper = mapper;
    }

    public async Task<MedicoResponseDto> CreateAsync(MedicoCreateDto medicoDto, Guid usuarioId)
    {
        var medico = _mapper.Map<Medico>(medicoDto);
        medico.CriadoPor = usuarioId;
        medico.CriadoEm = DateTime.UtcNow;
        medico.Ativo = true;

        await _medicoRepository.AddAsync(medico);
        await _medicoRepository.SaveChangesAsync();

        return await GetByIdAsync(medico.Id);
    }

    public async Task<IEnumerable<MedicoSummaryDto>> GetAllByClinicaAsync(Guid clinicaId)
    {
        var medicos = await _medicoRepository.GetAllByClinicaAsync(clinicaId);
        return _mapper.Map<IEnumerable<MedicoSummaryDto>>(medicos);
    }

    public async Task<IEnumerable<MedicoSummaryDto>> GetByEspecialidadeAndClinicaAsync(Guid clinicaId, Guid especialidadeId)
    {
        var medicos = await _medicoRepository.GetByEspecialidadeAndClinicaAsync(clinicaId, especialidadeId);
        return _mapper.Map<IEnumerable<MedicoSummaryDto>>(medicos);
    }

    public async Task<MedicoResponseDto> GetByIdAsync(Guid id)
    {
        var medico = await _medicoRepository.GetByIdAsync(id);
        return _mapper.Map<MedicoResponseDto>(medico);
    }

    public async Task<MedicoResponseDto> GetByCrmAsync(string crm)
    {
        var medico = await _medicoRepository.GetByCrmAsync(crm);
        return _mapper.Map<MedicoResponseDto>(medico);
    }

    public async Task<MedicoResponseDto> UpdateAsync(Guid id, MedicoUpdateDto medicoDto, Guid usuarioId)
    {
        var medico = await _medicoRepository.GetByIdAsync(id);
        if (medico == null) return null;

        _mapper.Map(medicoDto, medico);
        medico.AlteradoPor = usuarioId;
        medico.AlteradoEm = DateTime.UtcNow;

        _medicoRepository.Update(medico);
        await _medicoRepository.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var medico = await _medicoRepository.GetByIdAsync(id);
        if (medico == null) return false;

        _medicoRepository.Remove(medico);
        return await _medicoRepository.SaveChangesAsync();
    }
}
