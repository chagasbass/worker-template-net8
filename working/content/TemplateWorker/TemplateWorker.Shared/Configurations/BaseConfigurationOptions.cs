namespace VesteTemplateWorker.Shared.Configurations;

public record BaseConfigurationOptions
{
    public const string BaseConfig = "BaseConfiguration";

    public string? NomeProjeto { get; set; }
    public string? StringConexaoBancoDeDados { get; set; }
    public bool HabilitarMensagensDeLog { get; set; }

    public BaseConfigurationOptions() { }
}
