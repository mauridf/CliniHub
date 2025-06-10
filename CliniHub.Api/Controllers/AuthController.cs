using CliniHub.Application.Dtos.Auth;
using CliniHub.Application.Features.Authentication;
using CliniHub.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CliniHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var result = await _authService.LoginAsync(request.Email, request.Password);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        var user = new Usuario
        {
            Nome = request.Nome,
            Email = request.Email,
            UserName = request.Email,
            CPF = request.CPF,
            Genero = request.Genero,
            CriadoPor = Guid.Empty, // Será definido no serviço
            CriadoEm = DateTime.UtcNow
        };

        var result = await _authService.RegisterAsync(user, request.Password, request.Role);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return Ok();
    }
}