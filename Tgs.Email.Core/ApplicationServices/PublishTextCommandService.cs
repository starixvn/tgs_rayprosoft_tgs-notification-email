using System.Net.Mail;

using Tgs.ServiceUtility;

namespace Tgs.Email.Core.ApplicationServices;

internal class PublishTextCommandService(IEmailUtnsil utnsil) : ICommandService<PublishTextCommand>
{
    public ValueTask ExecuteAsync(PublishTextCommand command, CancellationToken cancellationToken = default)
    {
        if (!command.Recipients.Any())
        {
            return ValueTask.CompletedTask;
        }

        var to = new MailAddressCollection();
        command.Recipients.ToList().ForEach(to.Add);

        var cc = (MailAddressCollection?)null;
        if (command.Copies.Any())
        {
            cc = [];
            command.Copies.ToList().ForEach(c => cc.Add(c));
        }

        return (cc is null)
            ? utnsil.PublishTextAsync(
                to: to,
                subject: command.Subject,
                content: command.Content,
                cancellationToken: cancellationToken)
            : utnsil.PublishTextAsync(
                to: to,
                cc: cc,
                subject: command.Subject,
                content: command.Content,
                cancellationToken: cancellationToken);
    }
}