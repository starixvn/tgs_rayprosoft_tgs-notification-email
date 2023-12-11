using System.Net.Mail;

using NSubstitute;

using Tgs.Email.Core.ApplicationServices;

namespace Tgs.Email.Core.UnitTest.ApplicationServices;

public class PublishHtmlCommandServiceUnitTest
{
    private static readonly string[] Recipients = ["test@rayprosoft.com"];
    private static readonly string[] Copies = ["testcopy@rayprosoft.com"];

    [Fact]
    public async void Normal_Process()
    {
        var fakeEmailUtnsil = Substitute.For<IEmailUtnsil>();

        var command = new PublishHtmlCommand(
            Recipients: Recipients,
            Copies: Enumerable.Empty<string>(),
            Subject: "testSubject",
            Content: "testContent");

        var service = new PublishHtmlCommandService(
            fakeEmailUtnsil);

        await service.ExecuteAsync(command);

        _ = fakeEmailUtnsil
            .Received(1)
            .PublishHtmlAsync(
            to: Arg.Any<MailAddressCollection>(),
            subject: Arg.Is(command.Subject),
            content: Arg.Is(command.Content),
            cancellationToken: Arg.Any<CancellationToken>());
    }

    [Fact]
    public async void Normal_Process_Copy()
    {
        var fakeEmailUtnsil = Substitute.For<IEmailUtnsil>();

        var command = new PublishHtmlCommand(
            Recipients: Recipients,
            Copies: Copies,
            Subject: "testSubject",
            Content: "testContent");

        var service = new PublishHtmlCommandService(
            fakeEmailUtnsil);

        await service.ExecuteAsync(command);

        _ = fakeEmailUtnsil
            .Received(1)
            .PublishHtmlAsync(
            to: Arg.Any<MailAddressCollection>(),
            cc: Arg.Any<MailAddressCollection>(),
            subject: Arg.Is(command.Subject),
            content: Arg.Is(command.Content),
            cancellationToken: Arg.Any<CancellationToken>());
    }
}