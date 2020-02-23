using Newtonsoft.Json;
using Skyra.Core.Models.GameIntegrations.FFXIV;

namespace Skyra.Core.Database.Models
{
	/// <summary>
	/// 	A struct containing all the identifiable and useful base data about a Character.
	/// </summary>
	public struct UserGameIntegrationsFFXIV
	{
		public UserGameIntegrationsFFXIV(string name, string lodestoneID, DataCenters dc, Servers server, int slot)
		{
			Name = name;
			LodeStoneID = lodestoneID;
			DataCenter = dc;
			Server = server;
			Slot = slot;
		}

		/// <summary>
		/// 	A Characters full name.
		/// </summary>
		[JsonProperty("n")]
		public string Name { get; set; }

		[JsonIgnore]
		public string FirstName
			=> Name.Split(" ")[0];
		[JsonIgnore]
		public string LastName
			=> Name.Split(" ")[1];

		/// <summary>
		/// 	The Lodestone ID associated with the Character.
		/// </summary>
		[JsonProperty("lsid")]
		public string LodeStoneID { get; set; }

		/// <summary>
		/// 	The <see cref="DataCenters"/> on which the Character resides
		/// </summary>
		[JsonProperty("dc")]
		public DataCenters DataCenter { get; set; }

		/// <summary>
		/// 	The <see cref="Servers"/> in the <see cref="DataCenters"/> on which the Character resides
		/// </summary>
		[JsonProperty("s")]
		public Servers Server { get; set; }

		/// <summary>
		/// 	The relative slot in Skyra on which this Character struct is located.
		/// </summary>
		[JsonProperty("ps")]
		public int Slot { get; set; }
	}
}
