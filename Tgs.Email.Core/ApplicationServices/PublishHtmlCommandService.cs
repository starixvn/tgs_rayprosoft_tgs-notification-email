using System.Net.Mail;

using Tgs.ServiceUtility;

namespace Tgs.Email.Core.ApplicationServices;

internal class PublishHtmlCommandService(IEmailUtnsil utnsil) : ICommandService<PublishHtmlCommand>
{
    public ValueTask ExecuteAsync(PublishHtmlCommand command, CancellationToken cancellationToken = default)
    {
        if (!command.Recipients.Any())
        {
            return ValueTask.CompletedTask;
        }

        var recipients = new MailAddressCollection();
        command.Recipients.ToList().ForEach(recipients.Add);

        var carbonCopies = new MailAddressCollection();
        if (command.CarbonCopies.Any())
        {
            command.CarbonCopies.ToList().ForEach(carbonCopies.Add);
        }

        var blindCarbonCopies = new MailAddressCollection();
        if (command.BlindCarbonCopies.Any())
        {
            command.BlindCarbonCopies.ToList().ForEach(blindCarbonCopies.Add);
        }

        return utnsil.PublishHtmlAsync(
                to: recipients,
                cc: carbonCopies,
                bcc: blindCarbonCopies,
                subject: command.Subject,
                content: command.Content,
                cancellationToken: cancellationToken);
    }
}
