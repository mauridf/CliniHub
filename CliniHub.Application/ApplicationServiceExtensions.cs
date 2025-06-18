using System.Reflection;
using CliniHub.Application.Features.Appointments;
using CliniHub.Application.Features.Attendants;
using CliniHub.Application.Features.Authentication;
using CliniHub.Application.Features.Clinics;
using CliniHub.Application.Features.Consultation;
using CliniHub.Application.Features.Doctors;
using CliniHub.Application.Features.Patients;
using CliniHub.Application.Features.Users;
using CliniHub.Core.Domain.Repositories;
using CliniHub.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace CliniHub.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Registrar serviços de aplicação aqui
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IClinicaService, ClinicaService>();
        services.AddScoped<IAtendenteService, AtendenteService>();
        services.AddScoped<IMedicoService, MedicoService>();
        services.AddScoped<IEspecialidadeMedicaService, EspecialidadeMedicaService>();
        services.AddScoped<IPacienteService, PacienteService>();
        services.AddScoped<IAgendamentoService, AgendamentoService>();
        services.AddScoped<IConsultaService, ConsultaService>();

        // Registrar Repository
        services.AddScoped<IClinicaRepository, ClinicaRepository>();
        services.AddScoped<IAtendenteRepository, AtendenteRepository>();
        services.AddScoped<IMedicoRepository, MedicoRepository>();
        services.AddScoped<IEspecialidadeMedicaRepository, EspecialidadeMedicaRepository>();
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
        services.AddScoped<IConsultaRepository, ConsultaRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}