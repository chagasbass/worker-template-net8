namespace VesteTemplateWorker.Shared.Entities;

public class EmailInfo(string? conteudo, string? titulo, string? assunto)
{
    public string? Conteudo { get; set; } = conteudo;
    public string? Titulo { get; set; } = titulo;
    public string? Assunto { get; set; } = assunto;
}
