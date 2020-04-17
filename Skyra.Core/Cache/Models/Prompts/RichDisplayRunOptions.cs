using System;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class RichDisplayRunOptions
	{
		public bool Stop { get; set; } = true;
		public bool Jump { get; set; } = true;
		public bool FirstLast { get; set; } = true;

		public SendableMessage MessageContent { get; set; } = new SendableMessage
		{
			Embed = new Embed()
		};

		public int StartPage { get; set; } = 0;
		public TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(10);
	}
}
