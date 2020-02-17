using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	public sealed class CommandUsage
	{
		// TODO(TylerTron1998): Add reference to the Command structure.
		/// <summary>
		///     The name of the command that was run.
		/// </summary>
		[Column("id")]
		public string Id { get; set; } = null!;

		/// <summary>
		///     The amount of uses the command has, globally.
		/// </summary>
		[Column("uses")]
		public uint Uses { get; set; } = 0;
	}
}
