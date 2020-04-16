using System;

namespace Skyra.Core.Cache.Models.Prompts
{
	public struct CoreRichDisplayRunOptions
	{
		public CoreRichDisplayRunOptions(bool stop = true, bool jump = true, bool firstLast = true,
			string messageContent = "REACTION_HANDLER_PROMPT", uint startPage = 0,
			TimeSpan? duration = null)
		{
			Stop = stop;
			Jump = jump;
			FirstLast = firstLast;
			MessageContent = messageContent;
			StartPage = startPage;
			Duration = duration ?? TimeSpan.FromMinutes(10);
		}

		public bool Stop { get; set; }
		public bool Jump { get; set; }
		public bool FirstLast { get; set; }
		public string MessageContent { get; set; }
		public uint StartPage { get; set; }
		public TimeSpan Duration { get; set; }
	}
}
