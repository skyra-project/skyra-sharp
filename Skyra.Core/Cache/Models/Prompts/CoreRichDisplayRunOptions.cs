using System;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class CoreRichDisplayRunOptions
	{
		public bool Stop { get; set; } = true;
		public bool Jump { get; set; } = true;
		public bool FirstLast { get; set; } = true;
		public string MessageContent { get; set; } = "REACTION_HANDLER_PROMPT";
		public uint StartPage { get; set; } = 0;
		public TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(10);
	}
}
