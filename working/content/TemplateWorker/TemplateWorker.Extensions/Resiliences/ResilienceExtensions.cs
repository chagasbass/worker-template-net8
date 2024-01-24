namespace VesteTemplateWorker.Extensions.Resiliences
{
    public static class ResilienceExtensions
    {
        public static IServiceCollection AddApiResiliencesPatterns(this IServiceCollection services, IConfiguration configuration)
        {
            #region validação das configurações
            var retentativas = configuration["ResilienceConfiguration:QuantidadeDeRetentativas"];

            var quantidadeDeRetentativas = 1;

            if (!string.IsNullOrEmpty(retentativas))
            {
                quantidadeDeRetentativas = Int32.Parse(configuration["ResilienceConfiguration:QuantidadeDeRetentativas"]);
            }

            var nomeCliente = configuration["ResilenceConfiguration:NomeCliente"];

            nomeCliente ??= "restoqueWorker";

            #endregion

            if (quantidadeDeRetentativas == 0)
            {
                services.AddHttpClient(nomeCliente, options =>
                {
                    options.Timeout = TimeSpan.FromSeconds(15);
                    options.DefaultRequestHeaders.Add("accept", "application/json");
                })
                 .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
                 {
                     PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5)
                 })
                .SetHandlerLifetime(TimeSpan.FromMinutes(20));

                return services;
            }

            var serviceProvider = services.BuildServiceProvider();

            services.AddHttpClient(nomeCliente, options =>
            {
                options.Timeout = TimeSpan.FromSeconds(15);
                options.DefaultRequestHeaders.Add("accept", "application/json");
            })
                 .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
                 {
                     PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5)
                 })
                .SetHandlerLifetime(TimeSpan.FromMinutes(20))
                .AddPolicyHandler(ResiliencePolicies.GetApiRetryPolicy(serviceProvider, quantidadeDeRetentativas));

            return services;
        }
    }
}
