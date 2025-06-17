using AutoMapper;
using CliniHub.Application.Dtos.Doctors;
using CliniHub.Core.Domain.Entities;
using CliniHub.Infrastructure.Repositories;

namespace CliniHub.Application.Features.Doctors;

public class EspecialidadeMedicaService : IEspecialidadeMedicaService
{
    private readonly IEspecialidadeMedicaRepository _especialidadeRepository;
    private readonly IMapper _mapper;

    public EspecialidadeMedicaService(IEspecialidadeMedicaRepository especialidadeRepository, IMapper mapper)
    {
        _especialidadeRepository = especialidadeRepository;
        _mapper = mapper;
    }

    public async Task<EspecialidadeResponseDto> CreateAsync(EspecialidadeCreateDto especialidadeDto, Guid usuarioId)
    {
        var especialidade = _mapper.Map<EspecialidadeMedica>(especialidadeDto);
        especialidade.CriadoPor = usuarioId;
        especialidade.CriadoEm = DateTime.UtcNow;

        await _especialidadeRepository.AddAsync(especialidade);
        await _especialidadeRepository.SaveChangesAsync();

        return await GetByIdAsync(especialidade.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var especialidade = await _especialidadeRepository.GetByIdAsync(id);
        if (especialidade == null) return false;

        _especialidadeRepository.Remove(especialidade);
        return await _especialidadeRepository.SaveChangesAsync();
    }

    public async Task<EspecialidadeResponseDto> GetByIdAsync(Guid id)
    {
        var especialidade = await _especialidadeRepository.GetByIdAsync(id);
        return _mapper.Map<EspecialidadeResponseDto>(especialidade);
    }

    public async Task<IEnumerable<EspecialidadeResponseDto>> GetAllAsync()
    {
        var especialidades = await _especialidadeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EspecialidadeResponseDto>>(especialidades);
    }

    public async Task<EspecialidadeResponseDto> UpdateAsync(Guid id, EspecialidadeUpdateDto especialidadeDto, Guid usuarioId)
    {
        var especialidade = await _especialidadeRepository.GetByIdAsync(id);
        if (especialidade == null) return null;

        _mapper.Map(especialidadeDto, especialidade);
        especialidade.AlteradoPor = usuarioId;
        especialidade.AlteradoEm = DateTime.UtcNow;

        _especialidadeRepository.Update(especialidade);
        await _especialidadeRepository.SaveChangesAsync();

        return await GetByIdAsync(id);
    }
}
