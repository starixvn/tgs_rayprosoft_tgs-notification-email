namespace Tgs.Email.Core.ApplicationServices;

public record PublishHtmlCommand(
     IEnumerable<string> Recipients,
     IEnumerable<string> Copies,
     string Subject,
     string Content);