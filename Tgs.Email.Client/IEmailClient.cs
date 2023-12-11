namespace Tgs.Email.Client;

public interface IEmailClient
{
    ValueTask PublishTextAsync(IEnumerable<string> recipients, string subject, string content, CancellationToken cancellationToken = default);

    ValueTask PublishTextAsync(IEnumerable<string> recipients, IEnumerable<string> copies, string subject, string content, CancellationToken cancellationToken = default);

    ValueTask PublishHtmlAsync(IEnumerable<string> recipients, string subject, string content, CancellationToken cancellationToken = default);

    ValueTask PublishHtmlAsync(IEnumerable<string> recipients, IEnumerable<string> copies, string subject, string content, CancellationToken cancellationToken = default);
}