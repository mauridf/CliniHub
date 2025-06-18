using CliniHub.Application.Dtos.Consultas;
using CliniHub.Application.Features.Consultation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CliniHub.Api.Controllers;

[Route("api/clinicas/{clinicaId}/[controller]")]
[ApiController]
[Authorize]
public class ConsultasController : ControllerBase
{
    private readonly IConsultaService _consultaService;

    public ConsultasController(IConsultaService consultaService)
    {
        _consultaService = consultaService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,ClinicaAdmin,Medico")]
    public async Task<ActionResult<ConsultaResponseDto>> Create(
        Guid clinicaId,
        [FromBody] ConsultaCreateDto consultaDto)
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
            var consulta = await _consultaService.CreateAsync(consultaDto, usuarioId);
            return CreatedAtAction(
                nameof(GetById),
                new { clinicaId, id = consulta.Id },
                consulta);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConsultaResponseDto>>> GetAllByClinica(Guid clinicaId)
    {
        var consultas = await _consultaService.GetAllByClinicaAsync(clinicaId);
        return Ok(consultas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConsultaResponseDto>> GetById(Guid clinicaId, Guid id)
    {
        var consulta = await _consultaService.GetByIdAsync(id);
        if (consulta == null) return NotFound();

        // Verificar se a consulta pertence à clínica
        if (consulta.Agendamento.ClinicaId != clinicaId)
        {
            return BadRequest("Consulta não pertence à clínica especificada");
        }

        return Ok(consulta);
    }

    [HttpGet("pacientes/{pacienteId}")]
    public async Task<ActionResult<IEnumerable<ConsultaResponseDto>>> GetByPaciente(Guid clinicaId, Guid pacienteId)
    {
        var consultas = await _consultaService.GetByPacienteAsync(clinicaId, pacienteId);
        return Ok(consultas);
    }

    [HttpGet("medicos/{medicoId}")]
    public async Task<ActionResult<IEnumerable<ConsultaResponseDto>>> GetByMedico(Guid clinicaId, Guid medicoId)
    {
        var consultas = await _consultaService.GetByMedicoAsync(clinicaId, medicoId);
        return Ok(consultas);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,ClinicaAdmin,Medico")]
    public async Task<ActionResult<ConsultaResponseDto>> Update(
        Guid clinicaId,
        Guid id,
        [FromBody] ConsultaUpdateDto consultaDto)
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
            var consulta = await _consultaService.UpdateAsync(id, consultaDto, usuarioId);
            if (consulta == null) return NotFound();

            if (consulta.Agendamento.ClinicaId != clinicaId)
            {
                return BadRequest("Consulta não pertence à clínica especificada");
            }

            return Ok(consulta);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro interno");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,ClinicaAdmin")]
    public async Task<IActionResult> Delete(Guid clinicaId, Guid id)
    {
        try
        {
            var consulta = await _consultaService.GetByIdAsync(id);
            if (consulta == null) return NotFound();

            if (consulta.Agendamento.ClinicaId != clinicaId)
            {
                return BadRequest("Consulta não pertence à clínica especificada");
            }

            var result = await _consultaService.DeleteAsync(id);
            if (!result) return BadRequest("Não foi possível excluir a consulta");

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro interno");
        }
    }
}