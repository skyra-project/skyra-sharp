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
	public class SetLanguageCommand : StructureBase
	{
		public SetLanguageCommand(Client client) : base(client)
		{
		}

		public async Task RunAsync(CoreMessage message, [Argument(Maximum = 5)] string language)
		{
			await using var db = new SkyraDatabaseContext();

			var entity = await db.Guilds.FindAsync(message.GuildId);

			if (entity is null)
			{
				entity = new Guild {Id = (ulong) message.GuildId!, Language = language};
				await db.Guilds.AddAsync(entity);
			}
			else
			{
				entity.Language = language;
			}

			await db.SaveChangesAsync();
			await message.SendLocaleAsync(Client, "SetLanguage", new object?[] {language});
		}
	}
}
