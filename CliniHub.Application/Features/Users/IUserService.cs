using CliniHub.Application.Dtos.Auth;

namespace CliniHub.Application.Features.Users;

public interface IUserService
{
    Task<UsuarioResponseDto> GetByIdAsync(Guid id);
    Task<IEnumerable<UsuarioResponseDto>> GetAllAsync();
    Task<IEnumerable<UsuarioResponseDto>> GetByNameAsync(string name);
    Task<UsuarioResponseDto> UpdateAsync(Guid id, UsuarioUpdateDto userDto);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
}
