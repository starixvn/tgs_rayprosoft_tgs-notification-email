using System.Net.Mail;

namespace Tgs.Notification.Email.Core;

public interface IEmailUtnsil
{
    /// <summary>
    /// Publishes the text asynchronous.
    /// </summary>
    /// <param name="to">To（發送給）.</param>
    /// <param name="cc">The cc (副本).</param>
    /// <param name="bcc">The BCC.(密副本)</param>
    /// <param name="subject">The subject.</param>
    /// <param name="content">The content.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    ValueTask PublishTextAsync(MailAddressCollection to, MailAddressCollection cc, MailAddressCollection bcc, string subject, string content, CancellationToken cancellationToken = default);

    /// <summary>
    /// Publishes the HTML asynchronous.
    /// </summary>
    /// <param name="to">To（發送給）.</param>
    /// <param name="cc">The cc (副本).</param>
    /// <param name="bcc">The BCC.(密副本)</param>
    /// <param name="subject">The subject.</param>
    /// <param name="content">The content.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    ValueTask PublishHtmlAsync(MailAddressCollection to, MailAddressCollection cc, MailAddressCollection bcc, string subject, string content, CancellationToken cancellationToken = default);
}
