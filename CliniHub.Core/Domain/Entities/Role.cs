using CliniHub.Core.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace CliniHub.Core.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public RoleName Nome { get; set; }
    public string Descricao { get; set; }
    public Guid CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid? AlteradoPor { get; set; }
    public DateTime? AlteradoEm { get; set; }
}