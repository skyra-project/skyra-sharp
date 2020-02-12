using System.Linq;
using System.Threading.Tasks;
using Skyra.Framework.Structures.Base;
using Spectacles.NET.Types;

namespace Skyra.Framework.Structures
{
	public abstract class Monitor : Piece
	{
		public readonly Client Client;
		private readonly MessageType[] AllowedTypes;
		private readonly bool IgnoreBots;
		private readonly bool IgnoreSelf;
		private readonly bool IgnoreOthers;
		private readonly bool IgnoreWebhooks;
		private readonly bool IgnoreEdits;

		protected Monitor(Client client, MonitorOptions options)
		{
			Client = client;
			Name = options.Name;
			AllowedTypes = options.AllowedTypes;
			IgnoreBots = options.IgnoreBots;
			IgnoreSelf = options.IgnoreSelf;
			IgnoreOthers = options.IgnoreOthers;
			IgnoreWebhooks = options.IgnoreWebhooks;
			IgnoreEdits = options.IgnoreEdits;
		}

		public abstract Task<bool> Run(Message message);

		public bool ShouldRun(Message message)
		{
			return AllowedTypes.Contains(message.Type)
			       && !(IgnoreBots && message.Author.Bot)
			       // && !(IgnoreSelf && message.Author.Id == Client.User.Id)
			       // && !(IgnoreOthers && message.Author.Id != Client.User.Id)
			       && !(IgnoreWebhooks && message.WebhookId != null)
			       && !(IgnoreEdits && message.EditedTimestamp != null);
		}
	}

	public struct MonitorOptions
	{
		public string Name;
		public MessageType[] AllowedTypes;
		public bool IgnoreBots;
		public bool IgnoreSelf;
		public bool IgnoreOthers;
		public bool IgnoreWebhooks;
		public bool IgnoreEdits;

		public MonitorOptions(string name, MessageType[] allowedTypes = null, bool ignoreBots = true,
			bool ignoreSelf = true, bool ignoreOthers = true, bool ignoreWebhooks = true, bool ignoreEdits = true)
		{
			Name = name;
			AllowedTypes = allowedTypes ?? new[] {MessageType.DEFAULT};
			IgnoreBots = ignoreBots;
			IgnoreSelf = ignoreSelf;
			IgnoreOthers = ignoreOthers;
			IgnoreWebhooks = ignoreWebhooks;
			IgnoreEdits = ignoreEdits;
		}
	}
}
