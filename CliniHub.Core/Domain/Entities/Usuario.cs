using CliniHub.Core.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace CliniHub.Core.Domain.Entities;

public class Usuario : IdentityUser<Guid>
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string EnderecoCompleto { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public Genero Genero { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }
    public Atendente Atendente { get; set; }
    public Medico Medico { get; set; }
}