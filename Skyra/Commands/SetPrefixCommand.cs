using System.Diagnostics;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Skyra.Core.Database;
using Skyra.Core.Database.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Client = Skyra.Core.Client;

namespace Skyra.Commands
{
	[Command]
	public class SetPrefixCommand : StructureBase
	{
		public SetPrefixCommand(Client client) : base(client)
		{
		}

		public async Task RunAsync(CoreMessage message, [Argument(Maximum = 10)] string prefix)
		{
			Debug.Assert(message.GuildId != null, "message.GuildId != null");

			await using var db = new SkyraDatabaseContext();

			var entity = db.Guilds.Find(message.GuildId);

			if (entity is null)
			{
				entity = new Guild {Id = (ulong) message.GuildId, Prefix = prefix};
				await db.Guilds.AddAsync(entity);
			}
			else
			{
				entity.Prefix = prefix;
			}

			await db.SaveChangesAsync();

			await message.SendAsync(Client, $"Successfully set the prefix to `{prefix}`.");
		}
	}
}
