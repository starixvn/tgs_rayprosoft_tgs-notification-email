using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NSubstitute;

using Tgs.BrandGameCredits.IntegrationUnitTest;
using Tgs.Email.Client;
using Tgs.Email.Core.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.Email.IntegrationUnitTest;

public class EmailClientUnitTest
{
    [Fact]
    public async Task Publish_Html()
    {
        IEnumerable<string> recipients = new string[]
        {
            "test@rayprosoft.com"
        };
        IEnumerable<string> copies = Enumerable.Empty<string>();
        var subject = "testSubject";
        var content = "testContent";

        var web = new TestEmailApplication(builder =>
        {
            _ = builder.ConfigureServices(services =>
            {
                var fakeService = Substitute.For<ICommandService<PublishHtmlCommand>>();

                var command = new PublishHtmlCommand(
                    Recipients: recipients,
                    Copies: Enumerable.Empty<string>(),
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

        var client = new EmailClient(httpClient);

        await client.PublishHtmlAsync(
            recipients: recipients,
            subject: subject,
            content: content);
    }

    [Fact]
    public async Task Publish_Html_Copies()
    {
        IEnumerable<string> recipients = new string[]
        {
            "test@rayprosoft.com"
        };
        IEnumerable<string> copies = new string[]
        {
            "test@rayprosoft.com"
        };
        var subject = "testSubject";
        var content = "testContent";

        var web = new TestEmailApplication(builder =>
        {
            _ = builder.ConfigureServices(services =>
            {
                var fakeService = Substitute.For<ICommandService<PublishHtmlCommand>>();

                var command = new PublishHtmlCommand(
                    Recipients: recipients,
                    Copies: copies,
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

        var client = new EmailClient(httpClient);

        await client.PublishHtmlAsync(
            recipients: recipients,
            copies: copies,
            subject: subject,
            content: content);
    }

    [Fact]
    public async Task Publish_Text()
    {
        IEnumerable<string> recipients = new string[]
        {
            "test@rayprosoft.com"
        };
        IEnumerable<string> copies = Enumerable.Empty<string>();
        var subject = "testSubject";
        var content = "testContent";

        var web = new TestEmailApplication(builder =>
        {
            _ = builder.ConfigureServices(services =>
            {
                var fakeService = Substitute.For<ICommandService<PublishTextCommand>>();

                var command = new PublishTextCommand(
                    Recipients: recipients,
                    Copies: Enumerable.Empty<string>(),
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

        var client = new EmailClient(httpClient);

        await client.PublishTextAsync(
            recipients: recipients,
            subject: subject,
            content: content);
    }

    [Fact]
    public async Task Publish_Text_Copies()
    {
        IEnumerable<string> recipients = new string[]
        {
            "test@rayprosoft.com"
        };
        IEnumerable<string> copies = new string[]
        {
            "test@rayprosoft.com"
        };
        var subject = "testSubject";
        var content = "testContent";

        var web = new TestEmailApplication(builder =>
        {
            _ = builder.ConfigureServices(services =>
            {
                var fakeService = Substitute.For<ICommandService<PublishTextCommand>>();

                var command = new PublishTextCommand(
                    Recipients: recipients,
                    Copies: copies,
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

        var client = new EmailClient(httpClient);

        await client.PublishTextAsync(
            recipients: recipients,
            copies: copies,
            subject: subject,
            content: content);
    }
}