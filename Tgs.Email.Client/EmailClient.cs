using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Tgs.Email.Common.Models;

namespace Tgs.Email.Client;

internal class EmailClient(HttpClient httpClient) : IEmailClient
{
    private static readonly JsonSerializerOptions _SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };

    public async ValueTask PublishHtmlAsync(IEnumerable<string> recipients, string subject, string content, CancellationToken cancellationToken = default)
    {
        var add = new EmailPublish
        {
            Recipients = recipients,
            Copies = Enumerable.Empty<string>(),
            Subject = subject,
            Content = content,
        };
        var path = new Uri(httpClient.BaseAddress!, $"/api/email/html");
        var builder = new UriBuilder(path);
        string url = builder.ToString();
        var httpContent = new StringContent(JsonSerializer.Serialize(add, _SerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);

        _ = await httpClient.PostAsync(url, httpContent, cancellationToken)
            .ToAsyncEnumerable()
           .Do(response => response.EnsureSuccessStatusCode())
           .FirstAsync(cancellationToken: cancellationToken)
           .ConfigureAwait(false);
    }

    public async ValueTask PublishHtmlAsync(IEnumerable<string> recipients, IEnumerable<string> copies, string subject, string content, CancellationToken cancellationToken = default)
    {
        var add = new EmailPublish
        {
            Recipients = recipients,
            Copies = copies,
            Subject = subject,
            Content = content,
        };
        var path = new Uri(httpClient.BaseAddress!, $"/api/email/html");
        var builder = new UriBuilder(path);
        string url = builder.ToString();
        var httpContent = new StringContent(JsonSerializer.Serialize(add, _SerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);

        _ = await httpClient.PostAsync(url, httpContent, cancellationToken)
            .ToAsyncEnumerable()
           .Do(response => response.EnsureSuccessStatusCode())
           .FirstAsync(cancellationToken: cancellationToken)
           .ConfigureAwait(false);
    }

    public async ValueTask PublishTextAsync(IEnumerable<string> recipients, string subject, string content, CancellationToken cancellationToken = default)
    {
        var add = new EmailPublish
        {
            Recipients = recipients,
            Copies = Enumerable.Empty<string>(),
            Subject = subject,
            Content = content,
        };
        var path = new Uri(httpClient.BaseAddress!, $"/api/email/text");
        var builder = new UriBuilder(path);
        string url = builder.ToString();
        var httpContent = new StringContent(JsonSerializer.Serialize(add, _SerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);

        _ = await httpClient.PostAsync(url, httpContent, cancellationToken)
            .ToAsyncEnumerable()
           .Do(response => response.EnsureSuccessStatusCode())
           .FirstAsync(cancellationToken: cancellationToken)
           .ConfigureAwait(false);
    }

    public async ValueTask PublishTextAsync(IEnumerable<string> recipients, IEnumerable<string> copies, string subject, string content, CancellationToken cancellationToken = default)
    {
        var add = new EmailPublish
        {
            Recipients = recipients,
            Copies = copies,
            Subject = subject,
            Content = content,
        };
        var path = new Uri(httpClient.BaseAddress!, $"/api/email/text");
        var builder = new UriBuilder(path);
        string url = builder.ToString();
        var httpContent = new StringContent(JsonSerializer.Serialize(add, _SerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);

        _ = await httpClient.PostAsync(url, httpContent, cancellationToken)
            .ToAsyncEnumerable()
           .Do(response => response.EnsureSuccessStatusCode())
           .FirstAsync(cancellationToken: cancellationToken)
           .ConfigureAwait(false);
    }
}