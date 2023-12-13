using Microsoft.Extensions.Options;
using Tgs.Email.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationEmailClient(
        this IServiceCollection services,
        Action<EmailClientOptions, IServiceProvider> configure)
        =>
        services
        .AddOptions<EmailClientOptions>()
        .Configure(configure)
        .Services
        .AddHttpClient<IEmailClient, EmailClient>()
        .ConfigureHttpClient((sp, httpClient) =>
        {
            var options = sp.GetRequiredService<IOptions<EmailClientOptions>>().Value;
            httpClient.BaseAddress = options.BaseUrl;
        })
        .AddHttpMessageHandler<LoggingHandler>()
        .Services
        .AddTransient<LoggingHandler>();
}
