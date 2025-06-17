using CliniHub.Application.Dtos.Doctors;
using CliniHub.Application.Features.Doctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CliniHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EspecialidadeMedicaController : ControllerBase
    {
        private readonly IEspecialidadeMedicaService _especialidadeService;

        public EspecialidadeMedicaController(IEspecialidadeMedicaService especialidadeService)
        {
            _especialidadeService = especialidadeService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MedicoResponseDto>> Create([FromBody] EspecialidadeCreateDto especialidadeDto)
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
                var especialidade = await _especialidadeService.CreateAsync(especialidadeDto, usuarioId);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = especialidade.Id },
                    especialidade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadeResponseDto>> GetById(Guid id)
        {
            var especialidade = await _especialidadeService.GetByIdAsync(id);
            if (especialidade == null) return NotFound();

            return Ok(especialidade);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadeResponseDto>>> GetAll()
        {
            var especialidades = await _especialidadeService.GetAllAsync();
            return Ok(especialidades);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,ClinicaAdmin")]
        public async Task<ActionResult<EspecialidadeResponseDto>> Update(
            Guid id,
            [FromBody] EspecialidadeUpdateDto especialidadeDto)
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
                var especialidade = await _especialidadeService.UpdateAsync(id, especialidadeDto, usuarioId);
                if (especialidade == null) return NotFound();

                return Ok(especialidade);
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
                var especialidade = await _especialidadeService.GetByIdAsync(id);
                if (especialidade == null) return NotFound();

                var result = await _especialidadeService.DeleteAsync(id);
                if (!result) return BadRequest("Não foi possível excluir a especialidade médica");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno");
            }
        }
    }
}
