using System.Net.Mail;
using System.Net.Mime;
using System.Text;

using Microsoft.Extensions.Options;

namespace Tgs.Notification.Email.Core;

internal class EmailUtnsil(
	IOptions<EmailSettingsOptions> emailConfigAccessor,
	SmtpClient smtpClient) : IEmailUtnsil
{
	private readonly EmailSettingsOptions _emailSettings = emailConfigAccessor.Value;

	public async ValueTask PublishHtmlAsync(MailAddressCollection to, MailAddressCollection cc, MailAddressCollection bcc, string subject, string content, CancellationToken cancellationToken = default)
	{
		if (!to.Any())
		{
			return;
		}

		var message = new MailMessage(
		   from: $"{_emailSettings.FromName} <{_emailSettings.FromMail}>",
		   to: string.Join(",", to.Select(x => x.Address)))
		{
			Subject = subject,
			SubjectEncoding = Encoding.UTF8,
			BodyEncoding = Encoding.UTF8,
			IsBodyHtml = true,
		};

		if (cc.Any())
		{
			message.CC.Add(string.Join(",", cc.Select(x => x.Address)));
		}

		if (bcc.Any())
		{
			message.Bcc.Add(string.Join(",", bcc.Select(x => x.Address)));
		}

		message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content, Encoding.UTF8, MediaTypeNames.Text.Html));

		await smtpClient.SendMailAsync(
			message,
			cancellationToken)
			.ConfigureAwait(false);

		smtpClient.SendCompleted += (_, _) =>
		{
			smtpClient.Dispose();
			message.Dispose();
		};
	}

	public async ValueTask PublishTextAsync(MailAddressCollection to, MailAddressCollection cc, MailAddressCollection bcc, string subject, string content, CancellationToken cancellationToken = default)
	{
		if (!to.Any())
		{
			return;
		}

		var message = new MailMessage(
		   from: $"{_emailSettings.FromName} <{_emailSettings.FromMail}>",
		   to: string.Join(",", to.Select(x => x.Address)))
		{
			Subject = subject,
			SubjectEncoding = Encoding.UTF8,
			BodyEncoding = Encoding.UTF8,
		};

		if (cc.Any())
		{
			message.CC.Add(string.Join(",", cc.Select(x => x.Address)));
		}

		if (bcc.Any())
		{
			message.Bcc.Add(string.Join(",", bcc.Select(x => x.Address)));
		}

		message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content, Encoding.UTF8, MediaTypeNames.Text.Plain));

		await smtpClient.SendMailAsync(
			message,
			cancellationToken)
			.ConfigureAwait(false);

		smtpClient.SendCompleted += (_, _) =>
		{
			smtpClient.Dispose();
			message.Dispose();
		};
	}
}
