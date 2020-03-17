using System.Globalization;
using System.Threading.Tasks;
using Skyra.Core;
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
		public SetLanguageCommand(IClient client) : base(client)
		{
		}

		public async Task RunAsync(CoreMessage message, CultureInfo language)
		{
			await using var db = new SkyraDatabaseContext();

			var entity = await db.Guilds.FindAsync(message.GuildId);

			if (entity is null)
			{
				entity = new Guild {Id = (ulong) message.GuildId!, Language = language.Name};
				await db.Guilds.AddAsync(entity);
			}
			else
			{
				entity.Language = language.Name;
			}

			await db.SaveChangesAsync();
			await message.SendLocaleAsync(Client, "SetLanguage", new object?[] {language.Name});
		}
	}
}
