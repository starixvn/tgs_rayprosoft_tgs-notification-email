using System.Net;
using System.Net.Mail;

using Microsoft.Extensions.Options;

using Tgs.Notification.Email.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollecitonExtensions
{
	public static IServiceCollection AddNotificationEmail(
		this IServiceCollection services,
	   Action<EmailSettingsOptions, IServiceProvider> emailSettingsOptions)
		=> services
		.AddOptions<EmailSettingsOptions>()
		.Configure(emailSettingsOptions)
		.Services
		.AddTransient((sp) =>
		{
			var emailSettings = sp.GetRequiredService<IOptions<EmailSettingsOptions>>().Value;
			return new SmtpClient
			{
				Host = emailSettings.SmtpServer,
				Port = emailSettings.Port,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(
					emailSettings.FromMail,
					emailSettings.Password),
				EnableSsl = !string.IsNullOrEmpty(emailSettings.Password)
			};
		})
		.AddNotificationEmailCore();
}
