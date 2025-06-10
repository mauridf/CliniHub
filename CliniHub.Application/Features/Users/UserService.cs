using AutoMapper;
using CliniHub.Application.Dtos.Auth;
using CliniHub.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Application.Features.Users;

public class UserService : IUserService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<Usuario> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UsuarioResponseDto> GetByIdAsync(Guid id)
    {
        var user = await _userManager.Users
            .Include(u => u.Atendente)
            .Include(u => u.Medico)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return null;

        var userDto = _mapper.Map<UsuarioResponseDto>(user);
        userDto.Roles = await _userManager.GetRolesAsync(user);

        return userDto;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> GetAllAsync()
    {
        var users = await _userManager.Users
            .Include(u => u.Atendente)
            .Include(u => u.Medico)
            .ToListAsync();

        var userDtos = new List<UsuarioResponseDto>();

        foreach (var user in users)
        {
            var userDto = _mapper.Map<UsuarioResponseDto>(user);
            userDto.Roles = await _userManager.GetRolesAsync(user);
            userDtos.Add(userDto);
        }

        return userDtos;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> GetByNameAsync(string name)
    {
        var users = await _userManager.Users
            .Where(u => u.Nome.Contains(name))
            .Include(u => u.Atendente)
            .Include(u => u.Medico)
            .ToListAsync();

        var userDtos = new List<UsuarioResponseDto>();

        foreach (var user in users)
        {
            var userDto = _mapper.Map<UsuarioResponseDto>(user);
            userDto.Roles = await _userManager.GetRolesAsync(user);
            userDtos.Add(userDto);
        }

        return userDtos;
    }

    public async Task<UsuarioResponseDto> UpdateAsync(Guid id, UsuarioUpdateDto userDto)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return null;

        _mapper.Map(userDto, user);
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new ApplicationException($"Falha ao atualizar usuário: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return false;

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return false;

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.Succeeded;
    }
}