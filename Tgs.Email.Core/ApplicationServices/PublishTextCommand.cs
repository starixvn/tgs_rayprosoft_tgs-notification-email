namespace Tgs.Email.Core.ApplicationServices;

public record PublishTextCommand(
     IEnumerable<string> Recipients,
     IEnumerable<string> CarbonCopies,
     IEnumerable<string> BlindCarbonCopies,
     string Subject,
     string Content);
