using System.ComponentModel.DataAnnotations;

namespace Tgs.Notification.Email.Common.Models;

public record EmailPublish
{
	[Required]
	public IEnumerable<string> Recipients { get; init; } = default!;
	[Required]
	public IEnumerable<string> CarbonCopies { get; init; } = default!;
	[Required]
	public IEnumerable<string> BlindCarbonCopies { get; init; } = default!;
	[Required]
	public string Subject { get; init; } = default!;
	[Required]
	public string Content { get; init; } = default!;
}
