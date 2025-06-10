using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CliniHub.Core.Domain.Entities;
using CliniHub.Core.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CliniHub.Application.Features.Authentication;

public class AuthService : IAuthService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<Usuario> _signInManager;

    public AuthService(
        UserManager<Usuario> userManager,
        RoleManager<Role> roleManager,
        IConfiguration configuration,
        SignInManager<Usuario> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _signInManager = signInManager;
    }

    public async Task<AuthResponse> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            return new AuthResponse
            {
                Errors = new[] { "Email ou senha incorretos." },
                Success = false
            };
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new AuthResponse
        {
            Success = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }

    public async Task<RegistrationResponse> RegisterAsync(Usuario usuario, string password, RoleName role)
    {
        var existingUser = await _userManager.FindByEmailAsync(usuario.Email);

        if (existingUser != null)
        {
            return new RegistrationResponse
            {
                Success = false,
                Errors = new[] { "Já existe um usuário com este email." }
            };
        }

        var result = await _userManager.CreateAsync(usuario, password);

        if (!result.Succeeded)
        {
            return new RegistrationResponse
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        // Verificar se a role existe, caso contrário, criá-la
        var roleName = role.ToString();
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new Role
            {
                Name = roleName,
                Nome = role,
                Descricao = $"Role para {roleName}", // Adicione uma descrição padrão
                CriadoPor = usuario.Id,
                CriadoEm = DateTime.UtcNow
            });
        }

        await _userManager.AddToRoleAsync(usuario, roleName);

        return new RegistrationResponse { Success = true };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}