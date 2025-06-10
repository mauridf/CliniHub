using CliniHub.Application.Dtos.Clinics;
using CliniHub.Application.Features.Clinics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CliniHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicasController : ControllerBase
    {
        private readonly IClinicaService _clinicaService;
        private readonly ILogger<ClinicasController> _logger;

        public ClinicasController(IClinicaService clinicaService,ILogger<ClinicasController> logger)
        {
            _clinicaService = clinicaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicaSummaryDto>>> GetAll()
        {
            var clinicas = await _clinicaService.GetAllAsync();
            return Ok(clinicas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicaResponseDto>> GetById(Guid id)
        {
            var clinica = await _clinicaService.GetByIdAsync(id);
            if (clinica == null) return NotFound();
            return Ok(clinica);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClinicaResponseDto>> Create([FromBody] ClinicaCreateDto clinicaDto)
        {
            try
            {
                // 1. Validação do DTO
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // 2. Obter ID do usuário do token
                var userIdClaim = User.FindFirst("sub") ??
                                 User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var usuarioId))
                {
                    return Unauthorized("Não foi possível identificar o usuário");
                }

                // 3. Chamar o serviço
                var clinica = await _clinicaService.CreateAsync(clinicaDto, usuarioId);

                // 4. Retornar resposta
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = clinica.Id },
                    clinica);
            }
            catch (Exception ex)
            {
                // Logar o erro (adicionar ILogger no construtor)
                _logger.LogError(ex, "Erro ao criar clínica");
                return StatusCode(500, "Ocorreu um erro interno");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClinicaResponseDto>> Update(Guid id, [FromBody] ClinicaUpdateDto clinicaDto)
        {
            var clinica = await _clinicaService.UpdateAsync(id, clinicaDto);
            if (clinica == null) return NotFound();
            return Ok(clinica);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _clinicaService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/logo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateLogo(Guid id, [FromBody] string logoPath)
        {
            var result = await _clinicaService.UpdateLogoAsync(id, logoPath);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}