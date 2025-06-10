namespace CliniHub.Application.Dtos.Auth;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
}

public class RoleWithUsersDto : RoleDto
{
    public ICollection<UsuarioResponseDto> Usuarios { get; set; }
}
