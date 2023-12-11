using System.ComponentModel.DataAnnotations;

namespace Tgs.Email.Common.Models;

public record EmailPublish
{
    [Required]
    public IEnumerable<string> Recipients { get; init; } = default!;
    [Required]
    public IEnumerable<string> Copies { get; init; } = default!;
    [Required]
    public string Subject { get; init; } = default!;
    [Required]
    public string Content { get; init; } = default!;
}