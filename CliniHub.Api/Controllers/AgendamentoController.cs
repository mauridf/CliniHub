using CliniHub.Application.Dtos.Appointments;
using CliniHub.Application.Features.Appointments;
using CliniHub.Core.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CliniHub.Api.Controllers;

[Route("api/clinicas/{clinicaId}/[controller]")]
[ApiController]
[Authorize]
public class AgendamentosController : ControllerBase
{
    private readonly IAgendamentoService _agendamentoService;

    public AgendamentosController(IAgendamentoService agendamentoService)
    {
        _agendamentoService = agendamentoService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,ClinicaAdmin,Atendente")]
    public async Task<ActionResult<AgendamentoResponseDto>> Create(
        Guid clinicaId,
        [FromBody] AgendamentoCreateDto agendamentoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (agendamentoDto.ClinicaId != clinicaId)
        {
            return BadRequest("ID da clínica inconsistente");
        }

        var userIdClaim = User.FindFirst("sub") ?? User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var usuarioId))
        {
            return Unauthorized("Token inválido ou usuário não identificado");
        }

        try
        {
            var agendamento = await _agendamentoService.CreateAsync(agendamentoDto, usuarioId);
            return CreatedAtAction(
                nameof(GetById),
                new { clinicaId, id = agendamento.Id },
                agendamento);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro interno");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgendamentoResponseDto>>> GetAllByClinica(Guid clinicaId)
    {
        var agendamentos = await _agendamentoService.GetAllByClinicaAsync(clinicaId);
        return Ok(agendamentos);
    }

    [HttpGet("consultas")]
    public async Task<ActionResult<IEnumerable<AgendamentoResponseDto>>> GetConsultasByClinica(Guid clinicaId)
    {
        var agendamentos = await _agendamentoService.GetConsultasByClinicaAsync(clinicaId);
        return Ok(agendamentos);
    }

    [HttpGet("exames")]
    public async Task<ActionResult<IEnumerable<AgendamentoResponseDto>>> GetExamesByClinica(Guid clinicaId)
    {
        var agendamentos = await _agendamentoService.GetExamesByClinicaAsync(clinicaId);
        return Ok(agendamentos);
    }

    [HttpGet("agendados")]
    public async Task<ActionResult<IEnumerable<AgendamentoResponseDto>>> GetAgendadosByClinica(Guid clinicaId)
    {
        var agendamentos = await _agendamentoService.GetAgendadosByClinicaAsync(clinicaId);
        return Ok(agendamentos);
    }

    [HttpGet("realizados")]
    public async Task<ActionResult<IEnumerable<AgendamentoResponseDto>>> GetRealizadosByClinica(Guid clinicaId)
    {
        var agendamentos = await _agendamentoService.GetRealizadosByClinicaAsync(clinicaId);
        return Ok(agendamentos);
    }

    [HttpGet("cancelados")]
    public async Task<ActionResult<IEnumerable<AgendamentoResponseDto>>> GetCanceladosByClinica(Guid clinicaId)
    {
        var agendamentos = await _agendamentoService.GetCanceladosByClinicaAsync(clinicaId);
        return Ok(agendamentos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AgendamentoResponseDto>> GetById(Guid clinicaId, Guid id)
    {
        var agendamento = await _agendamentoService.GetByIdAsync(id);
        if (agendamento == null) return NotFound();

        if (agendamento.ClinicaId != clinicaId)
        {
            return BadRequest("Agendamento não pertence à clínica especificada");
        }

        return Ok(agendamento);
    }

    [HttpGet("pacientes/{pacienteId}")]
    public async Task<ActionResult<IEnumerable<AgendamentoResponseDto>>> GetByPaciente(Guid clinicaId, Guid pacienteId)
    {
        var agendamentos = await _agendamentoService.GetByPacienteAsync(clinicaId, pacienteId);
        return Ok(agendamentos);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,ClinicaAdmin,Atendente")]
    public async Task<ActionResult<AgendamentoResponseDto>> Update(
        Guid clinicaId,
        Guid id,
        [FromBody] AgendamentoUpdateDto agendamentoDto)
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
            var agendamento = await _agendamentoService.UpdateAsync(id, agendamentoDto, usuarioId);
            if (agendamento == null) return NotFound();

            if (agendamento.ClinicaId != clinicaId)
            {
                return BadRequest("Agendamento não pertence à clínica especificada");
            }

            return Ok(agendamento);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro interno");
        }
    }

    [HttpPut("{id}/status/{status}")]
    [Authorize(Roles = "Admin,ClinicaAdmin,Atendente")]
    public async Task<ActionResult<AgendamentoResponseDto>> UpdateStatus(
        Guid clinicaId,
        Guid id,
        StatusAgendamento status)
    {
        var userIdClaim = User.FindFirst("sub") ?? User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var usuarioId))
        {
            return Unauthorized("Token inválido ou usuário não identificado");
        }

        try
        {
            var agendamento = await _agendamentoService.UpdateStatusAsync(id, status, usuarioId);
            if (agendamento == null) return NotFound();

            if (agendamento.ClinicaId != clinicaId)
            {
                return BadRequest("Agendamento não pertence à clínica especificada");
            }

            return Ok(agendamento);
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
            var agendamento = await _agendamentoService.GetByIdAsync(id);
            if (agendamento == null) return NotFound();

            if (agendamento.ClinicaId != clinicaId)
            {
                return BadRequest("Agendamento não pertence à clínica especificada");
            }

            var result = await _agendamentoService.DeleteAsync(id);
            if (!result) return BadRequest("Não foi possível excluir o agendamento");

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro interno");
        }
    }
}