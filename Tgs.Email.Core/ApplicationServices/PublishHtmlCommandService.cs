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

        var to = new MailAddressCollection();
        command.Recipients.ToList().ForEach(to.Add);

        var cc = new MailAddressCollection();
        if (command.Copies.Any())
        {
            command.Recipients.ToList().ForEach(cc.Add);
        }

        return (!cc.Any())
            ? utnsil.PublishHtmlAsync(
                to: to!,
                subject: command.Subject,
                content: command.Content,
                cancellationToken: cancellationToken)
            : utnsil.PublishHtmlAsync(
                to: to!,
                cc: cc!,
                subject: command.Subject,
                content: command.Content,
                cancellationToken: cancellationToken);
    }
}