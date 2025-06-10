using AutoMapper;
using CliniHub.Application.Dtos.Clinics;
using CliniHub.Core.Domain.Entities;
using CliniHub.Core.Domain.Repositories;

namespace CliniHub.Application.Features.Clinics;

public class ClinicaService : IClinicaService
{
    private readonly IClinicaRepository _clinicaRepository;
    private readonly IMapper _mapper;

    public ClinicaService(IClinicaRepository clinicaRepository, IMapper mapper)
    {
        _clinicaRepository = clinicaRepository;
        _mapper = mapper;
    }

    public async Task<ClinicaResponseDto> GetByIdAsync(Guid id)
    {
        var clinica = await _clinicaRepository.GetByIdAsync(id);
        return _mapper.Map<ClinicaResponseDto>(clinica);
    }

    public async Task<IEnumerable<ClinicaSummaryDto>> GetAllAsync()
    {
        var clinicas = await _clinicaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ClinicaSummaryDto>>(clinicas);
    }

    public async Task<ClinicaResponseDto> CreateAsync(ClinicaCreateDto clinicaDto, Guid usuarioId)
    {
        if (clinicaDto == null)
        {
            throw new ArgumentNullException(nameof(clinicaDto));
        }

        var clinica = _mapper.Map<Clinica>(clinicaDto);
        clinica.CriadoPor = usuarioId;
        clinica.CriadoEm = DateTime.UtcNow;

        await _clinicaRepository.AddAsync(clinica);
        await _clinicaRepository.SaveChangesAsync();

        return _mapper.Map<ClinicaResponseDto>(clinica);
    }

    public async Task<ClinicaResponseDto> UpdateAsync(Guid id, ClinicaUpdateDto clinicaDto)
    {
        var clinica = await _clinicaRepository.GetByIdAsync(id);
        if (clinica == null) return null;

        _mapper.Map(clinicaDto, clinica);
        clinica.AlteradoEm = DateTime.UtcNow;

        await _clinicaRepository.UpdateAsync(clinica);
        await _clinicaRepository.SaveChangesAsync();

        return _mapper.Map<ClinicaResponseDto>(clinica);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var clinica = await _clinicaRepository.GetByIdAsync(id);
        if (clinica == null) return false;

        await _clinicaRepository.DeleteAsync(clinica);
        await _clinicaRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateLogoAsync(Guid id, string logoPath)
    {
        var clinica = await _clinicaRepository.GetByIdAsync(id);
        if (clinica == null) return false;

        clinica.Logotipo = logoPath;
        clinica.AlteradoEm = DateTime.UtcNow;

        await _clinicaRepository.UpdateAsync(clinica);
        await _clinicaRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ClinicaMedicosResponseDto>> GetClinicasWithMedicosAsync()
    {
        var clinicas = await _clinicaRepository.GetClinicasWithMedicosAsync();
        return _mapper.Map<IEnumerable<ClinicaMedicosResponseDto>>(clinicas);
    }

    public async Task<IEnumerable<ClinicaSummaryDto>> GetClinicasByUFAsync(string uf)
    {
        var clinicas = await _clinicaRepository.GetClinicasByUFAsync(uf);
        return _mapper.Map<IEnumerable<ClinicaSummaryDto>>(clinicas);
    }
}
