using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Database;
using Skyra.Core.Database.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Utils;

namespace Skyra.Commands
{
	[Command(Inhibitors = new[] {"Developer"})]
	public class SetPrefixCommand : StructureBase
	{
		public SetPrefixCommand(IClient client) : base(client)
		{
		}

		public async Task RunAsync([NotNull] CoreMessage message, [Argument(Maximum = 10)] string prefix)
		{
			await using var db = new SkyraDatabaseContext();
			await db.Guilds.UpdateOrCreateAsync((ulong) message.GuildId!, guild => { guild.Prefix = prefix; },
				guild => new Guild {Id = (ulong) message.GuildId!});
			await db.SaveChangesAsync();
			await message.SendLocaleAsync("SetPrefix", prefix);
		}
	}
}
