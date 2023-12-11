namespace Tgs.Email.Core.ApplicationServices;

public record PublishTextCommand(
     IEnumerable<string> Recipients,
     IEnumerable<string> Copies,
     string Subject,
     string Content);