namespace Skyra.Core.Structures
{
	public readonly ref struct EventOptions
	{
		public string Name { get; }

		public EventOptions(string name)
		{
			Name = name;
		}
	}
}
