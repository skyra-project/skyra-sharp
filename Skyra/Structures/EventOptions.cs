namespace Skyra.Structures
{
	public readonly ref struct EventOptions
	{
		public readonly string Name { get; }

		public EventOptions(string name)
		{
			Name = name;
		}
	}
}
