using System.ComponentModel.DataAnnotations;
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
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 3)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "CPF é obrigatório")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 caracteres")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    [StringLength(100, MinimumLength = 8)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Gênero é obrigatório")]
    public Genero Genero { get; set; }

    [Required(ErrorMessage = "Tipo de usuário é obrigatório")]
    public RoleName Role { get; set; }

    [StringLength(200)]
    public string? EnderecoCompleto { get; set; }

    [StringLength(2)]
    public string? UF { get; set; }

    [StringLength(8)]
    public string? CEP { get; set; }
}