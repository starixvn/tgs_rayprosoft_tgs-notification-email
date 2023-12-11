namespace Tgs.Email.Core;

public record EmailSettingsOptions
{
    public string FromMail { get; set; } = default!;

    public string FromName { get; set; } = default!;

    public string SmtpServer { get; set; } = default!;

    public int Port { get; set; }

    public string Password { get; set; } = default!;
}