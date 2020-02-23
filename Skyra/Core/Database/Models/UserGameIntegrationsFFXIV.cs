using Newtonsoft.Json;
using Skyra.Core.Models.GameIntegrations.FFXIV;

namespace Skyra.Core.Database.Models
{
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

		[JsonProperty("n")]
		public string Name { get; set; }

		[JsonIgnore]
		public string FirstName
			=> Name.Split(" ")[0];
		[JsonIgnore]
		public string LastName
			=> Name.Split(" ")[1];

		[JsonProperty("lsid")]
		public string LodeStoneID { get; set; }

		[JsonProperty("dc")]
		public DataCenters DataCenter { get; set; }

		[JsonProperty("s")]
		public Servers Server { get; set; }

		[JsonProperty("ps")]
		public int Slot { get; set; }
	}
}
