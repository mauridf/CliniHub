using AutoMapper;
using CliniHub.Core.Domain.Entities;
using CliniHub.Application.Dtos.Appointments;
using CliniHub.Application.Dtos.Auth;
using CliniHub.Application.Dtos.Consultas;
using CliniHub.Application.Dtos.Exames;
using CliniHub.Application.Dtos.Receitas;
using CliniHub.Application.Dtos.Atestados;
using CliniHub.Application.Dtos.Medicamentos;
using CliniHub.Application.Dtos.Clinics;
using CliniHub.Application.Dtos.Doctors;
using CliniHub.Application.Dtos.Patients;
using CliniHub.Application.Dtos.Attendants;

namespace CliniHub.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamentos de Usuário
        CreateMap<Usuario, UsuarioResponseDto>();
        CreateMap<UsuarioCreateDto, Usuario>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        CreateMap<UsuarioUpdateDto, Usuario>();

        //Mapeamentos de Atendente
        CreateMap<AtendenteCreateDto, Atendente>();
        CreateMap<Atendente, AtendenteResponseDto>();

        // Mapeamentos de Agendamento
        CreateMap<Agendamento, AgendamentoResponseDto>();
        CreateMap<AgendamentoCreateDto, Agendamento>();
        CreateMap<Agendamento, AgendamentoCalendarDto>()
            .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.DataHora))
            .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.DataHora.AddMinutes(30)))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => $"{src.Paciente.Nome} - {src.Medico.Usuario.Nome}"));

        // Mapeamentos de Consulta
        CreateMap<Consulta, ConsultaResponseDto>();
        CreateMap<ConsultaCreateDto, Consulta>();
        CreateMap<ConsultaUpdateDto, Consulta>();

        // Mapeamentos de TipoExame
        CreateMap<TipoExame, TipoExameDto>();
        CreateMap<TipoExameDto, TipoExame>();

        // Mapeamentos de PedidoExame
        CreateMap<PedidoExame, PedidoExameResponseDto>()
            .ForMember(dest => dest.TipoExameNome, opt => opt.MapFrom(src => src.TipoExame.Nome));
        CreateMap<PedidoExameDto, PedidoExame>();

        // Mapeamentos de Receita
        CreateMap<Receita, ReceitaResponseDto>();
        CreateMap<ReceitaCreateDto, Receita>();

        // Mapeamentos de ItemReceita
        CreateMap<ItemReceita, ItemReceitaResponseDto>()
            .ForMember(dest => dest.MedicamentoNome, opt => opt.MapFrom(src => src.Medicamento.NomeComercial));
        CreateMap<ItemReceitaDto, ItemReceita>();

        // Mapeamentos de Medicamento
        CreateMap<Medicamento, MedicamentoDto>();
        CreateMap<MedicamentoDto, Medicamento>();

        // Mapeamentos de Atestado
        CreateMap<Atestado, AtestadoResponseDto>();
        CreateMap<AtestadoCreateDto, Atestado>();

        // Mapeamentos de Laudo
        CreateMap<Laudo, LaudoResponseDto>()
            .ForMember(dest => dest.Medico, opt => opt.MapFrom(src => src.Medico))
            .ForMember(dest => dest.Paciente, opt => opt.MapFrom(src => src.Paciente));
        CreateMap<LaudoCreateDto, Laudo>();

        // Mapeamentos de DisponibilidadeMedico
        CreateMap<DisponibilidadeMedico, DisponibilidadeResponseDto>()
            .ForMember(dest => dest.HorarioInicio, opt => opt.MapFrom(src => src.HorarioInicio.ToString(@"hh\:mm")))
            .ForMember(dest => dest.HorarioFim, opt => opt.MapFrom(src => src.HorarioFim.ToString(@"hh\:mm")));
        CreateMap<DisponibilidadeCreateDto, DisponibilidadeMedico>();

        // Mapeamentos de BloqueioAgendaMedico
        CreateMap<BloqueioAgendaMedico, BloqueioResponseDto>()
            .ForMember(dest => dest.Medico, opt => opt.MapFrom(src => new MedicoSummaryDto
            {
                Id = src.Medico.Id,
                Nome = src.Medico.Usuario.Nome,
                CRM = src.Medico.CRM
            }));
        CreateMap<BloqueioCreateDto, BloqueioAgendaMedico>();

        // Mapeamentos de Clinica
        CreateMap<Clinica, ClinicaResponseDto>();
        CreateMap<ClinicaCreateDto, Clinica>();
        CreateMap<ClinicaUpdateDto, Clinica>();
        CreateMap<Clinica, ClinicaSummaryDto>();
        CreateMap<Clinica, ClinicaMedicosResponseDto>()
            .ForMember(dest => dest.Medicos, opt => opt.MapFrom(src => src.Medicos));

        // Mapeamentos de Medico
        CreateMap<Medico, MedicoResponseDto>()
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario))
            .ForMember(dest => dest.Especialidade, opt => opt.MapFrom(src => src.EspecialidadeMedica));
        CreateMap<MedicoCreateDto, Medico>();
        CreateMap<MedicoUpdateDto, Medico>();
        CreateMap<Medico, MedicoSummaryDto>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
            .ForMember(dest => dest.Especialidade, opt => opt.MapFrom(src => src.EspecialidadeMedica.Nome));

        // Mapeamentos de Paciente
        CreateMap<Paciente, PacienteResponseDto>();
        CreateMap<PacienteCreateDto, Paciente>();
        CreateMap<PacienteUpdateDto, Paciente>();
        CreateMap<Paciente, PacienteSummaryDto>();

        // Mapeamentos de EspecialidadeMedica
        CreateMap<EspecialidadeMedica, EspecialidadeResponseDto>();
        CreateMap<EspecialidadeCreateDto, EspecialidadeMedica>();
    }
}