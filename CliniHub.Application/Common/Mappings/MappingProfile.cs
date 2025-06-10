using AutoMapper;
using CliniHub.Core.Domain.Entities;
using CliniHub.Application.Dtos.Appointments;
using CliniHub.Application.Dtos.Auth;

namespace CliniHub.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioResponseDto>();
        CreateMap<UsuarioCreateDto, Usuario>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

        CreateMap<UsuarioUpdateDto, Usuario>();

        CreateMap<Agendamento, AgendamentoResponseDto>();
        CreateMap<AgendamentoCreateDto, Agendamento>();

        // Adicione todos os outros mapeamentos necessários
    }
}