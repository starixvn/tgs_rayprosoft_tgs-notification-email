using System.Net.Mail;

using Tgs.ServiceUtility;

using Tgs.Notification.Email.Core;

using Tgs.Notification.Email.Core.ApplicationServices;

namespace Tgs.Notification.Email.Core.ApplicationServices;

internal class PublishTextCommandService(IEmailUtnsil utnsil) : ICommandService<PublishTextCommand>
{
    public ValueTask ExecuteAsync(PublishTextCommand command, CancellationToken cancellationToken = default)
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

        return utnsil.PublishTextAsync(
                to: recipients,
                cc: carbonCopies,
                bcc: blindCarbonCopies,
                subject: command.Subject,
                content: command.Content,
                cancellationToken: cancellationToken);
    }
}
