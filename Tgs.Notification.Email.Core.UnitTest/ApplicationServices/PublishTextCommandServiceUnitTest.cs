using System.Net.Mail;

using NSubstitute;

using Tgs.Notification.Email.Core;
using Tgs.Notification.Email.Core.ApplicationServices;

namespace Tgs.Notification.Email.Core.UnitTest.ApplicationServices;

public class PublishTextCommandServiceUnitTest
{
    private static readonly string[] _Recipients = ["test@rayprosoft.com"];
    private static readonly string[] _CarbonCopies = ["tech@rayprosoft.com"];
    private static readonly string[] _BlindCarbonCopies = ["hank.wu@rayprosoft.com"];

    [Fact]
    public async void Normal_Process()
    {
        var fakeEmailUtnsil = Substitute.For<IEmailUtnsil>();

        var command = new PublishTextCommand(
            Recipients: _Recipients,
            CarbonCopies: Enumerable.Empty<string>(),
            BlindCarbonCopies: Enumerable.Empty<string>(),
            Subject: "testSubject",
            Content: "testContent");

        var service = new PublishTextCommandService(
            fakeEmailUtnsil);

        await service.ExecuteAsync(command);

        _ = fakeEmailUtnsil
            .Received(1)
            .PublishTextAsync(
            to: Arg.Any<MailAddressCollection>(),
            cc: Arg.Any<MailAddressCollection>(),
            bcc: Arg.Any<MailAddressCollection>(),
            subject: Arg.Is(command.Subject),
            content: Arg.Is(command.Content),
            cancellationToken: Arg.Any<CancellationToken>());
    }

    [Fact]
    public async void Normal_Process_Carbon_Copies()
    {
        var fakeEmailUtnsil = Substitute.For<IEmailUtnsil>();

        var command = new PublishTextCommand(
            Recipients: _Recipients,
            CarbonCopies: _CarbonCopies,
            BlindCarbonCopies: Enumerable.Empty<string>(),
            Subject: "testSubject",
            Content: "testContent");

        var service = new PublishTextCommandService(
            fakeEmailUtnsil);

        await service.ExecuteAsync(command);

        _ = fakeEmailUtnsil
            .Received(1)
            .PublishTextAsync(
            to: Arg.Any<MailAddressCollection>(),
            cc: Arg.Any<MailAddressCollection>(),
            bcc: Arg.Any<MailAddressCollection>(),
            subject: Arg.Is(command.Subject),
            content: Arg.Is(command.Content),
            cancellationToken: Arg.Any<CancellationToken>());
    }

    [Fact]
    public async void Normal_Process_Blind_Carbon_Copies()
    {
        var fakeEmailUtnsil = Substitute.For<IEmailUtnsil>();

        var command = new PublishTextCommand(
            Recipients: _Recipients,
            CarbonCopies: _CarbonCopies,
            BlindCarbonCopies: _BlindCarbonCopies,
            Subject: "testSubject",
            Content: "testContent");

        var service = new PublishTextCommandService(
            fakeEmailUtnsil);

        await service.ExecuteAsync(command);

        _ = fakeEmailUtnsil
            .Received(1)
            .PublishTextAsync(
            to: Arg.Any<MailAddressCollection>(),
            cc: Arg.Any<MailAddressCollection>(),
            bcc: Arg.Any<MailAddressCollection>(),
            subject: Arg.Is(command.Subject),
            content: Arg.Is(command.Content),
            cancellationToken: Arg.Any<CancellationToken>());
    }
}
