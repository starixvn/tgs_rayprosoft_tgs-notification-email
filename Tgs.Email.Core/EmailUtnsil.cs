using System.Net.Mail;
using System.Net.Mime;
using System.Text;

using Microsoft.Extensions.Options;

namespace Tgs.Email.Core;

internal class EmailUtnsil(
    IOptions<EmailSettingsOptions> emailConfigAccessor,
    SmtpClient smtpClient) : IEmailUtnsil
{
    private readonly EmailSettingsOptions emailSettings = emailConfigAccessor.Value;

    public async ValueTask PublishHtmlAsync(MailAddressCollection to, MailAddressCollection cc, string subject, string content, CancellationToken cancellationToken = default)
    {
        if (!to.Any())
        {
            return;
        }

        var message = new MailMessage(
           from: $"{emailSettings.FromName} <{emailSettings.FromMail}>",
           to: string.Join(",", to.Select(x => x.Address)))
        {
            Subject = subject,
            SubjectEncoding = Encoding.UTF8,
            BodyEncoding = Encoding.UTF8,
            IsBodyHtml = true,
        };
        message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content, Encoding.UTF8, MediaTypeNames.Text.Html));
        message.CC.Add(string.Join(",", cc.Select(x => x.Address)));

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

    public async ValueTask PublishHtmlAsync(MailAddressCollection to, string subject, string content, CancellationToken cancellationToken = default)

    {
        if (!to.Any())
        {
            return;
        }

        var message = new MailMessage(
           from: $"{emailSettings.FromName} <{emailSettings.FromMail}>",
           to: string.Join(",", to.Select(x => x.Address)))
        {
            Subject = subject,
            SubjectEncoding = Encoding.UTF8,
            BodyEncoding = Encoding.UTF8,
            IsBodyHtml = true,
        };
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

    public async ValueTask PublishTextAsync(MailAddressCollection to, MailAddressCollection cc, string subject, string content, CancellationToken cancellationToken = default)
    {
        if (!to.Any())
        {
            return;
        }

        var message = new MailMessage(
           from: $"{emailSettings.FromName} <{emailSettings.FromMail}>",
           to: string.Join(",", to.Select(x => x.Address)))
        {
            Subject = subject,
            SubjectEncoding = Encoding.UTF8,
            BodyEncoding = Encoding.UTF8,
        };
        message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content, Encoding.UTF8, MediaTypeNames.Text.Plain));
        message.CC.Add(string.Join(",", cc.Select(x => x.Address)));

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

    public async ValueTask PublishTextAsync(MailAddressCollection to, string subject, string content, CancellationToken cancellationToken = default)

    {
        if (!to.Any())
        {
            return;
        }

        var message = new MailMessage(
           from: $"{emailSettings.FromName} <{emailSettings.FromMail}>",
           to: string.Join(",", to.Select(x => x.Address)))
        {
            Subject = subject,
            SubjectEncoding = Encoding.UTF8,
            BodyEncoding = Encoding.UTF8,
        };
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