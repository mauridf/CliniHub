namespace CliniHub.Application.Dtos.Doctors;

public class EspecialidadeCreateDto
{
    public string Nome { get; set; }
    public string Observacao { get; set; }
}

public class EspecialidadeResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Observacao { get; set; }
}
