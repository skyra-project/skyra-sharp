using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Skyra.Core.Database;
using Skyra.Core.Database.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Client = Skyra.Core.Client;

namespace Skyra.Commands
{
	[Command(Inhibitors = new[] {"Developer"})]
	public class SetPrefixCommand : StructureBase
	{
		public SetPrefixCommand(Client client) : base(client)
		{
		}

		public async Task RunAsync(CoreMessage message, [Argument(Maximum = 10)] string prefix)
		{
			await using var db = new SkyraDatabaseContext();

			var entity = await db.Guilds.FindAsync(message.GuildId);

			if (entity is null)
			{
				entity = new Guild {Id = (ulong) message.GuildId!, Prefix = prefix};
				await db.Guilds.AddAsync(entity);
			}
			else
			{
				entity.Prefix = prefix;
			}

			await db.SaveChangesAsync();
			await message.SendLocaleAsync(Client, "SetPrefix", new object?[] {prefix});
		}
	}
}
