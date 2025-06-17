using System.Security.Claims;
using CliniHub.Application.Dtos.Attendants;
using CliniHub.Application.Features.Attendants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CliniHub.Api.Controllers;

[ApiController]
[Route("api/clinicas/{clinicaId}/[controller]")]
public class AtendentesController : ControllerBase
{
    private readonly IAtendenteService _atendenteService;
    private readonly ILogger<AtendentesController> _logger;

    public AtendentesController(IAtendenteService atendenteService, ILogger<AtendentesController> logger)
    {
        _atendenteService = atendenteService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AtendenteResponseDto>>> GetByClinica(Guid clinicaId)
    {
        var atendentes = await _atendenteService.GetByClinicaAsync(clinicaId);
        return Ok(atendentes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AtendenteResponseDto>> GetById(Guid id)
    {
        var atendente = await _atendenteService.GetByIdAsync(id);
        if (atendente == null) return NotFound();
        return Ok(atendente);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,ClinicaAdmin")]
    public async Task<ActionResult<AtendenteResponseDto>> Create(
    Guid clinicaId,
    [FromBody] AtendenteCreateDto atendenteDto)
    {
        // 1. Validação do DTO
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (atendenteDto.ClinicaId != clinicaId)
        {
            return BadRequest("ID da clínica inconsistente");
        }

        // 2. Obter ID do usuário de forma segura
        var userIdClaim = User.FindFirst("sub") ?? User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var usuarioId))
        {
            return Unauthorized("Token inválido ou usuário não identificado");
        }

        try
        {
            // 3. Chamar o serviço
            var atendente = await _atendenteService.CreateAsync(atendenteDto, usuarioId);

            // 4. Retornar resposta
            return CreatedAtAction(
                nameof(GetById),
                new { clinicaId, id = atendente.Id },
                atendente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar atendente");
            return StatusCode(500, "Ocorreu um erro interno");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,ClinicaAdmin")]
    public async Task<ActionResult<AtendenteResponseDto>> Update(
        Guid clinicaId,
        Guid id,
        [FromBody] AtendenteCreateDto atendenteDto)
    {
        if (atendenteDto.ClinicaId != clinicaId)
        {
            return BadRequest("ID da clínica inconsistente");
        }

        var atendente = await _atendenteService.UpdateAsync(id, atendenteDto);
        if (atendente == null) return NotFound();
        return Ok(atendente);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,ClinicaAdmin")]
    public async Task<IActionResult> Delete(Guid clinicaId, Guid id)
    {
        var result = await _atendenteService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}