namespace Skyra.Core.Cache.Models.Prompts
{
	public struct CoreRichDisplayEmojis
	{
		public CoreRichDisplayEmojis(string? first = "%E2%8F%AE", string? back = "%E2%97%80",
			string? forward = "%E2%96%B6", string? last = "%E2%8F%AD", string? jump = "%F0%9F%94%A2",
			string? info = "%E2%84%B9", string? stop = "%E2%8F%B9")
		{
			First = first;
			Back = back;
			Forward = forward;
			Last = last;
			Jump = jump;
			Info = info;
			Stop = stop;
		}

		public string? First { get; set; }
		public string? Back { get; set; }
		public string? Forward { get; set; }
		public string? Last { get; set; }
		public string? Jump { get; set; }
		public string? Info { get; set; }
		public string? Stop { get; set; }
	}
}
