﻿using System.ComponentModel.DataAnnotations;
using CliniHub.Application.Dtos.Doctors;

namespace CliniHub.Application.Dtos.Clinics;

public class ClinicaCreateDto
{
    public string Nome { get; set; }
    public string CNPJ { get; set; }
    public string EnderecoCompleto { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    [Required]
    public string Logotipo { get; set; } = "default-logo.png";
}

public class ClinicaUpdateDto
{
    public string Nome { get; set; }
    public string EnderecoCompleto { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
}

public class ClinicaResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string CNPJ { get; set; }
    public string EnderecoCompleto { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string? Logotipo { get; set; }
}

public class ClinicaSummaryDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string UF { get; set; }
    public string Telefone { get; set; }
}

public class ClinicaMedicosResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string UF { get; set; }
    public IEnumerable<MedicoSummaryDto> Medicos { get; set; }
}