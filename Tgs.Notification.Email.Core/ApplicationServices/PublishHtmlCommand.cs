namespace Tgs.Notification.Email.Core.ApplicationServices;

public record PublishHtmlCommand(
	 IEnumerable<string> Recipients,
	 IEnumerable<string> CarbonCopies,
	 IEnumerable<string> BlindCarbonCopies,
	 string Subject,
	 string Content);
