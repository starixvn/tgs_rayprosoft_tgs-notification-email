using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Sentry;

using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Net;

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


		var mailMessageJson = JsonSerializer.Serialize(new
		{
			From = message.From.ToString(),
			To = string.Join(", ", message.To.Select(x => x.Address)),
			CC = string.Join(", ", message.CC.Select(x => x.Address)),
			BCC = string.Join(", ", message.Bcc.Select(x => x.Address)),
			Subject = message.Subject,
			Body = content,
		});

		var smtpClientJson = JsonSerializer.Serialize(new
		{
			Host = smtpClient.Host,
			Port = smtpClient.Port,
			EnableSsl = smtpClient.EnableSsl,
			Credentials = smtpClient.Credentials is NetworkCredential credential
				? new { credential.UserName, credential.Password }
				: null
		});

		try
		{
			await smtpClient.SendMailAsync(
			message,
			cancellationToken)
			.ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			var formattedMessage = $"EXCEPTION MESSAGE:\n{ex}\n\n" +
						   $"EMAIL MESSAGE:\n{mailMessageJson}\n\n" +
						   $"SMTP CLIENT:\n{smtpClientJson}";
			SentrySdk.CaptureException(new Exception(formattedMessage));
		}

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
