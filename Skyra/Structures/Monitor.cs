using System.Linq;
using System.Threading.Tasks;
using Skyra.Structures.Base;
using Spectacles.NET.Types;

namespace Skyra.Structures
{
	public abstract class Monitor : Piece
	{
		public readonly Client Client;
		private readonly MessageType[] _allowedTypes;
		private readonly bool _ignoreBots;
		private readonly bool _ignoreSelf;
		private readonly bool _ignoreOthers;
		private readonly bool _ignoreWebhooks;
		private readonly bool _ignoreEdits;

		protected Monitor(Client client, MonitorOptions options)
		{
			Client = client;
			Name = options.Name;
			_allowedTypes = options.AllowedTypes;
			_ignoreBots = options.IgnoreBots;
			_ignoreSelf = options.IgnoreSelf;
			_ignoreOthers = options.IgnoreOthers;
			_ignoreWebhooks = options.IgnoreWebhooks;
			_ignoreEdits = options.IgnoreEdits;
		}

		public abstract Task<bool> Run(Message message);

		public bool ShouldRun(Message message)
		{
			return _allowedTypes.Contains(message.Type)
			       && !(_ignoreBots && message.Author.Bot)
			       // && !(IgnoreSelf && message.Author.Id == Client.User.Id)
			       // && !(IgnoreOthers && message.Author.Id != Client.User.Id)
			       && !(_ignoreWebhooks && message.WebhookId != null)
			       && !(_ignoreEdits && message.EditedTimestamp != null);
		}
	}
}
