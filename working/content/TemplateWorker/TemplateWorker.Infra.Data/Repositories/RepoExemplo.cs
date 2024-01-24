namespace VesteTemplateWorker.Infra.Data.Repositories;

/// <summary>
/// Repositorio de exemplo Injetar sempre o  BaseConfigurationOptions para recuperar a conexão do appsettings
/// </summary>
/// <param name="options"></param>
internal class RepoExemplo(IOptions<BaseConfigurationOptions> options)
{

    async Task ExecutarAsync()
    {
        using var conexao = new SqlConnection(options.Value.StringConexaoBancoDeDados);

        await conexao.ExecuteAsync("query");
    }

}
