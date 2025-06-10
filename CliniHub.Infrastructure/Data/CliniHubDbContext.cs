using CliniHub.Core.Domain.Entities;
using CliniHub.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CliniHub.Infrastructure.Data;

public class CliniHubDbContext : IdentityDbContext<Usuario, Role, Guid>
{
    public CliniHubDbContext(DbContextOptions<CliniHubDbContext> options)
        : base(options)
    {
    }

    public DbSet<Clinica> Clinicas { get; set; }
    public DbSet<Atendente> Atendentes { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<EspecialidadeMedica> EspecialidadesMedicas { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    public DbSet<Laudo> Laudos { get; set; }
    public DbSet<DisponibilidadeMedico> DisponibilidadesMedicos { get; set; }
    public DbSet<BloqueioAgendaMedico> BloqueiosAgendaMedicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar configurações
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        modelBuilder.ApplyConfiguration(new ClinicaConfiguration());
        modelBuilder.ApplyConfiguration(new AtendenteConfiguration());
        modelBuilder.ApplyConfiguration(new MedicoConfiguration());
        modelBuilder.ApplyConfiguration(new EspecialidadeMedicaConfiguration());
        modelBuilder.ApplyConfiguration(new PacienteConfiguration());
        modelBuilder.ApplyConfiguration(new AgendamentoConfiguration());
        modelBuilder.ApplyConfiguration(new LaudoConfiguration());
        modelBuilder.ApplyConfiguration(new DisponibilidadeMedicoConfiguration());
        modelBuilder.ApplyConfiguration(new BloqueioAgendaMedicoConfiguration());

        // Configurar enums como strings
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Genero)
            .HasConversion<string>();

        modelBuilder.Entity<Agendamento>()
            .Property(a => a.Tipo)
            .HasConversion<string>();

        modelBuilder.Entity<Agendamento>()
            .Property(a => a.Status)
            .HasConversion<string>();

        modelBuilder.Entity<DisponibilidadeMedico>()
            .Property(d => d.DiaSemana)
            .HasConversion<string>();

        modelBuilder.Entity<Role>()
            .Property(r => r.Nome)
            .HasConversion<string>();
    }
}