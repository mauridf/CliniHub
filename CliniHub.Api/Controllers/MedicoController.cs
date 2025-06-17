using CliniHub.Application.Dtos.Doctors;
using CliniHub.Application.Features.Doctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CliniHub.Api.Controllers
{
    [Route("api/clinicas/{clinicaId}/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicosController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,ClinicaAdmin")]
        public async Task<ActionResult<MedicoResponseDto>> Create(
            Guid clinicaId,
            [FromBody] MedicoCreateDto medicoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (medicoDto.ClinicaId != clinicaId)
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
                var medico = await _medicoService.CreateAsync(medicoDto, usuarioId);
                return CreatedAtAction(
                    nameof(GetById),
                    new { clinicaId, id = medico.Id },
                    medico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicoSummaryDto>>> GetAllByClinica(Guid clinicaId)
        {
            var medicos = await _medicoService.GetAllByClinicaAsync(clinicaId);
            return Ok(medicos);
        }

        [HttpGet("especialidades/{especialidadeId}")]
        public async Task<ActionResult<IEnumerable<MedicoSummaryDto>>> GetByEspecialidade(
            Guid clinicaId,
            Guid especialidadeId)
        {
            var medicos = await _medicoService.GetByEspecialidadeAndClinicaAsync(clinicaId, especialidadeId);
            return Ok(medicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoResponseDto>> GetById(Guid clinicaId, Guid id)
        {
            var medico = await _medicoService.GetByIdAsync(id);
            if (medico == null) return NotFound();

            if (medico.ClinicaId != clinicaId)
            {
                return BadRequest("Médico não pertence à clínica especificada");
            }

            return Ok(medico);
        }

        [HttpGet("crm/{crm}")]
        public async Task<ActionResult<MedicoResponseDto>> GetByCrm(Guid clinicaId, string crm)
        {
            var medico = await _medicoService.GetByCrmAsync(crm);
            if (medico == null) return NotFound();

            if (medico.ClinicaId != clinicaId)
            {
                return BadRequest("Médico não pertence à clínica especificada");
            }

            return Ok(medico);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,ClinicaAdmin")]
        public async Task<ActionResult<MedicoResponseDto>> Update(
            Guid clinicaId,
            Guid id,
            [FromBody] MedicoUpdateDto medicoDto)
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
                var medico = await _medicoService.UpdateAsync(id, medicoDto, usuarioId);
                if (medico == null) return NotFound();

                if (medico.ClinicaId != clinicaId)
                {
                    return BadRequest("Médico não pertence à clínica especificada");
                }

                return Ok(medico);
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
                var medico = await _medicoService.GetByIdAsync(id);
                if (medico == null) return NotFound();

                if (medico.ClinicaId != clinicaId)
                {
                    return BadRequest("Médico não pertence à clínica especificada");
                }

                var result = await _medicoService.DeleteAsync(id);
                if (!result) return BadRequest("Não foi possível excluir o médico");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno");
            }
        }
    }
}