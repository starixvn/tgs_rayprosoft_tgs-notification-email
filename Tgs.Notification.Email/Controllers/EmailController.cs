using Microsoft.AspNetCore.Mvc;
using Tgs.Notification.Email.Common.Models;
using Tgs.Notification.Email.Core.ApplicationServices;

using Tgs.ServiceUtility;

namespace Tgs.Notification.Email.Controllers;

[Route("[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
	/// <summary>
	/// Texts the asynchronous.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="source">The source.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b><br />
	///
	///     Post
	///     {
	///         "recipients": [
	///             "gordon.hung@rayprosoft.com"
	///         ],
	///         "carbonCopies": [
	///             "tech@rayprosoft.com"
	///         ],
	///         "blindCarbonCopies": [
	///             "hank.wu@rayprosoft.com"
	///         ],
	///         "subject": "[Licensee][Brand] 加值通知 Warning: Balance low",
	///         "content": "敬愛的夥伴您好，\r\n您的平台目前剩餘額度不足，為避免影響會員遊戲體驗，請盡速聯繫貴司對應商務進行加值，謝謝您。\r\nDear Partner,\r\nWe wanted to inform you that your site currently has reached a high balance usage. To maintain the stability and quality of your gaming experience, we kindly recommend reaching out to your designated business contact for a top-up. Thank you."
	///     }
	/// </remarks>
	[HttpPost("text")]
	public ValueTask TextAsync(
		[FromServices] ICommandService<PublishTextCommand> service,
		EmailPublish source)
		=> service.ExecuteAsync(
			command: new PublishTextCommand(
				Recipients: source.Recipients,
				CarbonCopies: source.CarbonCopies,
				BlindCarbonCopies: source.BlindCarbonCopies,
				Subject: source.Subject,
				Content: source.Content),
			cancellationToken: HttpContext.RequestAborted);

	/// <summary>
	/// HTMLs the asynchronous.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="source">The source.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b><br />
	///
	///     Post
	///     {
	///         "recipients": [
	///             "gordon.hung@rayprosoft.com"
	///         ],
	///         "carbonCopies": [
	///             "tech@rayprosoft.com"
	///         ],
	///         "blindCarbonCopies": [
	///             "hank.wu@rayprosoft.com"
	///         ],
	///         "subject": "[Licensee][Brand] 加值通知 Warning: Balance low",
	///         "content": "敬愛的夥伴您好，<br/>您的平台目前剩餘額度不足，為避免影響會員遊戲體驗，請盡速聯繫貴司對應商務進行加值，謝謝您。<br/>Dear Partner,<br/>We wanted to inform you that your site currently has reached a high balance usage. To maintain the stability and quality of your gaming experience, we kindly recommend reaching out to your designated business contact for a top-up. Thank you."
	///     }
	/// </remarks>
	[HttpPost("html")]
	public ValueTask HtmlAsync(
		[FromServices] ICommandService<PublishHtmlCommand> service,
		EmailPublish source)
		=> service.ExecuteAsync(
			command: new PublishHtmlCommand(
				Recipients: source.Recipients,
				CarbonCopies: source.CarbonCopies,
				BlindCarbonCopies: source.BlindCarbonCopies,
				Subject: source.Subject,
				Content: source.Content),
			cancellationToken: HttpContext.RequestAborted);
}
