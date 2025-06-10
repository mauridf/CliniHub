using CliniHub.Core.Domain.Entities;
using CliniHub.Core.Domain.Enums;

namespace CliniHub.Application.Features.Authentication;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(string email, string password);
    Task<RegistrationResponse> RegisterAsync(Usuario usuario, string password, RoleName role);
    Task LogoutAsync();
}

public class AuthResponse
{
    public string Token { get; set; }
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
}

public class RegistrationResponse
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
}