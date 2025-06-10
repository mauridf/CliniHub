using CliniHub.Core.Domain.Enums;

namespace CliniHub.Application.Dtos.Auth;

public class UsuarioCreateDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Password { get; set; }
    public Genero Genero { get; set; }
    public string EnderecoCompleto { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
}

public class UsuarioUpdateDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public Genero Genero { get; set; }
    public string EnderecoCompleto { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UsuarioResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public Genero Genero { get; set; }
    public ICollection<string> Roles { get; set; }
}

public class RegisterDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Password { get; set; }
    public Genero Genero { get; set; }
    public RoleName Role { get; set; }
}
