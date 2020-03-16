using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skyra.Notifi.Controllers
{
	[Route("twitch")]
	public class TwitchController : Controller
	{
		[HttpGet("stream/{id:int?}")]
		public ActionResult<string> Challenge([FromQuery(Name = "hub.mode")] string mode, [FromQuery(Name = "hub.challenge")] string challenge)
		{
			switch (mode)
			{
				case "denied":
					// TODO: Use mime type constants
					HttpContext.Response.Headers.Append("Content-Type", "text/plain");
					return Ok(challenge ?? "ok");
				case "unsubscribe":
				case "subscribe":
					// TODO: Use mime type constants
					HttpContext.Response.Headers.Append("Content-Type", "text/plain");
					return Ok(challenge);
				default:
					return "Well... Isn't this a pain in the ass";
			}
		}

	}
}
