using CliniHub.Application.Dtos.Patients;
using CliniHub.Application.Features.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CliniHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PacientesController : ControllerBase
{
    private readonly IPacienteService _pacienteService;

    public PacientesController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,ClinicaAdmin,Atendente")]
    public async Task<ActionResult<PacienteResponseDto>> Create([FromBody] PacienteCreateDto pacienteDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst("sub") ?? User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var usuarioId))
        {
            return Unauthorized("Token inválido ou usuário não identificado");
        }

        try
        {
            var paciente = await _pacienteService.CreateAsync(pacienteDto, usuarioId);
            return CreatedAtAction(
                nameof(GetById),
                new { id = paciente.Id },
                paciente);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro interno");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PacienteSummaryDto>>> GetAll()
    {
        var pacientes = await _pacienteService.GetAllAsync();
        return Ok(pacientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PacienteResponseDto>> GetById(Guid id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);
        if (paciente == null) return NotFound();

        return Ok(paciente);
    }

    [HttpGet("nome/{nome}")]
    public async Task<ActionResult<IEnumerable<PacienteSummaryDto>>> GetByNome(string nome)
    {
        var pacientes = await _pacienteService.GetByNomeAsync(nome);
        return Ok(pacientes);
    }

    [HttpGet("cpf/{cpf}")]
    public async Task<ActionResult<PacienteResponseDto>> GetByCpf(string cpf)
    {
        var paciente = await _pacienteService.GetByCpfAsync(cpf);
        if (paciente == null) return NotFound();

        return Ok(paciente);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,ClinicaAdmin,Atendente")]
    public async Task<ActionResult<PacienteResponseDto>> Update(
        Guid id,
        [FromBody] PacienteUpdateDto pacienteDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst("sub") ?? User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var usuarioId))
        {
            return Unauthorized("Token inválido ou usuário não identificado");
        }

        try
        {
            var paciente = await _pacienteService.UpdateAsync(id, pacienteDto, usuarioId);
            if (paciente == null) return NotFound();

            return Ok(paciente);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro interno");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,ClinicaAdmin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _pacienteService.DeleteAsync(id);
            if (!result) return BadRequest("Não foi possível excluir o paciente");

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro interno");
        }
    }
}