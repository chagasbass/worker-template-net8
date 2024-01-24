namespace VesteTemplateWorker.Shared.Configurations;

public record ResilienceConfigurationOptions
{
    public const string ResilienciaConfig = "ResilienceConfiguration";
    public int QuantidadeDeRetentativas { get; set; }
    public string? NomeCliente { get; set; }

    public ResilienceConfigurationOptions() { }
}
