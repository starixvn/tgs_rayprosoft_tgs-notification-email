using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
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

		await TestSendMail();
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

	[HttpGet("TestSendMail")]
	public async Task<string> TestSendMail()
	{
		var smtpClient = new SmtpClient
		{
			Host = "10.40.40.45",
			Port = 25,
			UseDefaultCredentials = false,
			EnableSsl = false,
		};

		var message = new MailMessage(
	   from: "support2@vgamesuat.com",
	   to: "ricky.nguyen@sabawork.com,troy.vo@starixsoft.com,ricky.nguyen@lumiplus.vn")
		{
			Subject = "Test UAT Send mail at" + DateTime.Now,
			SubjectEncoding = Encoding.UTF8,
			BodyEncoding = Encoding.UTF8
		};
		message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("Test UAT Send mail at" + DateTime.Now, Encoding.UTF8, MediaTypeNames.Text.Plain));

		await smtpClient.SendMailAsync(message).ConfigureAwait(false);

		smtpClient.SendCompleted += (_, _) =>
		{
			smtpClient.Dispose();
			message.Dispose();
		};

		return "ok";
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
				Content: ""));
	}
}
