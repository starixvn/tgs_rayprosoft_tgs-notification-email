using Microsoft.Extensions.Options;
using Tgs.Email.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationEmailClient(
        this IServiceCollection services,
        Action<NotificationEmailClientOptions, IServiceProvider> configure)
        =>
        services
        .AddOptions<NotificationEmailClientOptions>()
        .Configure(configure)
        .Services
        .AddHttpClient<INotificationEmailClient, NotificationEmailClient>()
        .ConfigureHttpClient((sp, httpClient) =>
        {
            var options = sp.GetRequiredService<IOptions<NotificationEmailClientOptions>>().Value;
            httpClient.BaseAddress = options.BaseUrl;
        })
        .AddHttpMessageHandler<LoggingHandler>()
        .Services
        .AddTransient<LoggingHandler>();
}
