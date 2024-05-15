namespace Tgs.Email.Client;

public interface INotificationEmailClient
{
	ValueTask PublishTextAsync(IEnumerable<string> recipients, string subject, string content, IEnumerable<string>? carbonCopies = default, IEnumerable<string>? blindCarbonCopies = default, CancellationToken cancellationToken = default);

	ValueTask PublishHtmlAsync(IEnumerable<string> recipients, string subject, string content, IEnumerable<string>? carbonCopies = default, IEnumerable<string>? blindCarbonCopies = default, CancellationToken cancellationToken = default);
}
