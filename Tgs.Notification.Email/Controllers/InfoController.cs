using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenTelemetry.Resources;
using Tgs.Notification.Email.Core;
using Tgs.Notification.Email.Core.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.Notification.Email.Controllers;

[Route("[controller]")]
[ApiController]
public class InfoController : ControllerBase
{
	[HttpGet]
	public async Task<object> GetAsync(
		[FromServices] IWebHostEnvironment _hostingEnvironment,
		[FromServices] ICommandService<PublishTextCommand> service,
		IOptions<EmailSettingsOptions> emailSettings)
	{
		var hostName = Dns.GetHostName();
		var hostEntry = await Dns.GetHostEntryAsync(hostName).ConfigureAwait(false);
		var hostIp = Array.Find(hostEntry.AddressList,
			x => x.AddressFamily == AddressFamily.InterNetwork);

		await TestSendMailDirect(service, emailSettings.Value);
		//await TestSendMailWithProxy(service, emailSettings.Value);

		return new
		{
			Environment.MachineName,
			HostName = hostName,
			HostIp = hostIp?.ToString() ?? string.Empty,
			Environment = _hostingEnvironment.EnvironmentName,
			OsVersion = $"{Environment.OSVersion}",
			Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
			ProcessCount = Environment.ProcessorCount
		};
	}

	private async Task TestSendMailDirect(ICommandService<PublishTextCommand> service, EmailSettingsOptions emailSettings)
	{
		var smtpClient = new SmtpClient
		{
			Host = emailSettings.SmtpServer,
			Port = emailSettings.Port,
			UseDefaultCredentials = false,
			Credentials = new NetworkCredential(
					emailSettings.Username,
					emailSettings.Password),
			EnableSsl = !string.IsNullOrEmpty(emailSettings.Password),
		};

		await service.ExecuteAsync(
			command: new PublishTextCommand(
				Recipients: ["ricky.nguyen@sabawork.com"],
				CarbonCopies: ["ricky.nguyen@sabawork.com"],
				BlindCarbonCopies: ["ricky.nguyen@sabawork.com"],
		Subject: "Test TestSendMailDirect",
				Content: "Test TestSendMailDirect"));
	}

	private async Task TestSendMailWithProxy(ICommandService<PublishTextCommand> service, EmailSettingsOptions emailSettings)
	{
		var smtpClient = new SmtpClient
		{
			Host = emailSettings.SmtpServer,
			Port = emailSettings.Port,
			UseDefaultCredentials = false,
			Credentials = new NetworkCredential(
					emailSettings.Username,
					emailSettings.Password,
					"http://lisasqd.nebula.prod:8801"),
			EnableSsl = !string.IsNullOrEmpty(emailSettings.Password),
		};

		await service.ExecuteAsync(
			command: new PublishTextCommand(
				Recipients: ["ricky.nguyen@lumiplus.vn"],
				CarbonCopies: ["ricky.nguyen@lumiplus.vn"],
				BlindCarbonCopies: ["ricky.nguyen@lumiplus.vn"],
		Subject: "Test TestSendMailWithProxy",
				Content: "Test TestSendMailWithProxy"));
	}
}
