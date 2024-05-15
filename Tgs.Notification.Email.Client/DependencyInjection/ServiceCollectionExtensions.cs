using Tgs.Email.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddNotificationEmailClient(this IServiceCollection services, Action<IHttpClientBuilder> httpClientConfigure)
	{
		httpClientConfigure(services.AddHttpClient<INotificationEmailClient, NotificationEmailClient>());

		return services;
	}
}
