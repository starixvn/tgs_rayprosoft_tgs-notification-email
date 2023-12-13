using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NSubstitute;
using Tgs.Email.Client;
using Tgs.Notification.Email.Core.ApplicationServices;

using Tgs.ServiceUtility;

namespace Tgs.Notification.Email.IntegrationUnitTest;

public class NotificationEmailClientUnitTest
{
    private static readonly string[] _Recipients = ["gordon.hung@rayprosoft.com"];
    private static readonly string[] _CarbonCopies = ["tech@rayprosoft.com"];
    private static readonly string[] _BlindCarbonCopies = ["hank.wu@rayprosoft.com"];

    [Fact]
    public async Task Publish_Html_Recipients()
    {
        var subject = "testSubject";
        var content = "testContent";

        var web = new TestNotificationEmailApplication(builder =>
        {
            _ = builder.ConfigureServices(services =>
            {
                var fakeService = Substitute.For<ICommandService<PublishHtmlCommand>>();

                var command = new PublishHtmlCommand(
                    Recipients: _Recipients,
                    CarbonCopies: Enumerable.Empty<string>(),
                    BlindCarbonCopies: Enumerable.Empty<string>(),
                    Subject: subject,
                    Content: content);
                _ = fakeService
                    .ExecuteAsync(
                    command: Arg.Is(command),
                    cancellationToken: Arg.Any<CancellationToken>());

                _ = services.AddTransient(_ => fakeService);
            });
        });

        var fakeLoggingHandler = new LoggingHandler(web.Services.GetRequiredService<ILogger<LoggingHandler>>());

        var httpClient = web.CreateDefaultClient(fakeLoggingHandler);

        var client = new NotificationEmailClient(httpClient);

        await client.PublishHtmlAsync(
            recipients: _Recipients,
            subject: subject,
            content: content);
    }

    [Fact]
    public async Task Publish_Html_CarbonCopies()
    {
        var subject = "testSubject";
        var content = "testContent";

        var web = new TestNotificationEmailApplication(builder =>
        {
            _ = builder.ConfigureServices(services =>
            {
                var fakeService = Substitute.For<ICommandService<PublishHtmlCommand>>();

                var command = new PublishHtmlCommand(
                    Recipients: _Recipients,
                    CarbonCopies: _CarbonCopies,
                    BlindCarbonCopies: Enumerable.Empty<string>(),
                    Subject: subject,
                    Content: content);
                _ = fakeService
                    .ExecuteAsync(
                    command: Arg.Is(command),
                    cancellationToken: Arg.Any<CancellationToken>());

                _ = services.AddTransient(_ => fakeService);
            });
        });

        var fakeLoggingHandler = new LoggingHandler(web.Services.GetRequiredService<ILogger<LoggingHandler>>());

        var httpClient = web.CreateDefaultClient(fakeLoggingHandler);

        var client = new NotificationEmailClient(httpClient);

        await client.PublishHtmlAsync(
            recipients: _Recipients,
            subject: subject,
            content: content,
            carbonCopies: _CarbonCopies);
    }

    [Fact]
    public async Task Publish_Html_BlindCarbonCopies()
    {
        var subject = "testSubject";
        var content = "testContent";

        var web = new TestNotificationEmailApplication(builder =>
        {
            _ = builder.ConfigureServices(services =>
            {
                var fakeService = Substitute.For<ICommandService<PublishHtmlCommand>>();

                var command = new PublishHtmlCommand(
                    Recipients: _Recipients,
                    CarbonCopies: _CarbonCopies,
                    BlindCarbonCopies: _BlindCarbonCopies,
                    Subject: subject,
                    Content: content);
                _ = fakeService
                    .ExecuteAsync(
                    command: Arg.Is(command),
                    cancellationToken: Arg.Any<CancellationToken>());

                _ = services.AddTransient(_ => fakeService);
            });
        });

        var fakeLoggingHandler = new LoggingHandler(web.Services.GetRequiredService<ILogger<LoggingHandler>>());

        var httpClient = web.CreateDefaultClient(fakeLoggingHandler);

        var client = new NotificationEmailClient(httpClient);

        await client.PublishHtmlAsync(
            recipients: _Recipients,
            subject: subject,
            content: content,
            carbonCopies: _CarbonCopies,
            blindCarbonCopies: _BlindCarbonCopies);
    }

    [Fact]
    public async Task Publish_Text_Recipients()
    {
        var subject = "testSubject";
        var content = "testContent";

        var web = new TestNotificationEmailApplication(builder =>
        {
            _ = builder.ConfigureServices(services =>
            {
                var fakeService = Substitute.For<ICommandService<PublishTextCommand>>();

                var command = new PublishTextCommand(
                    Recipients: _Recipients,
                    CarbonCopies: Enumerable.Empty<string>(),
                    BlindCarbonCopies: Enumerable.Empty<string>(),
                    Subject: subject,
                    Content: content);
                _ = fakeService
                    .ExecuteAsync(
                    command: Arg.Is(command),
                    cancellationToken: Arg.Any<CancellationToken>());

                _ = services.AddTransient(_ => fakeService);
            });
        });

        var fakeLoggingHandler = new LoggingHandler(web.Services.GetRequiredService<ILogger<LoggingHandler>>());

        var httpClient = web.CreateDefaultClient(fakeLoggingHandler);

        var client = new NotificationEmailClient(httpClient);

        await client.PublishTextAsync(
            recipients: _Recipients,
            subject: subject,
            content: content);
    }

    [Fact]
    public async Task Publish_Text_CarbonCopies()
    {
        var subject = "testSubject";
        var content = "testContent";

        var web = new TestNotificationEmailApplication(builder =>
        {
            _ = builder.ConfigureServices(services =>
            {
                var fakeService = Substitute.For<ICommandService<PublishTextCommand>>();

                var command = new PublishTextCommand(
                    Recipients: _Recipients,
                    CarbonCopies: _CarbonCopies,
                    BlindCarbonCopies: Enumerable.Empty<string>(),
                    Subject: subject,
                    Content: content);
                _ = fakeService
                    .ExecuteAsync(
                    command: Arg.Is(command),
                    cancellationToken: Arg.Any<CancellationToken>());

                _ = services.AddTransient(_ => fakeService);
            });
        });

        var fakeLoggingHandler = new LoggingHandler(web.Services.GetRequiredService<ILogger<LoggingHandler>>());

        var httpClient = web.CreateDefaultClient(fakeLoggingHandler);

        var client = new NotificationEmailClient(httpClient);

        await client.PublishTextAsync(
            recipients: _Recipients,
            subject: subject,
            content: content,
            carbonCopies: _CarbonCopies);
    }

    [Fact]
    public async Task Publish_Text_BlindCarbonCopies()
    {
        var subject = "testSubject";
        var content = "testContent";

        var web = new TestNotificationEmailApplication(builder =>
        {
            _ = builder.ConfigureServices(services =>
            {
                var fakeService = Substitute.For<ICommandService<PublishTextCommand>>();

                var command = new PublishTextCommand(
                    Recipients: _Recipients,
                    CarbonCopies: _CarbonCopies,
                    BlindCarbonCopies: _BlindCarbonCopies,
                    Subject: subject,
                    Content: content);
                _ = fakeService
                    .ExecuteAsync(
                    command: Arg.Is(command),
                    cancellationToken: Arg.Any<CancellationToken>());

                _ = services.AddTransient(_ => fakeService);
            });
        });

        var fakeLoggingHandler = new LoggingHandler(web.Services.GetRequiredService<ILogger<LoggingHandler>>());

        var httpClient = web.CreateDefaultClient(fakeLoggingHandler);

        var client = new NotificationEmailClient(httpClient);

        await client.PublishTextAsync(
            recipients: _Recipients,
            subject: subject,
            content: content,
            carbonCopies: _CarbonCopies,
            blindCarbonCopies: _BlindCarbonCopies);
    }
}
