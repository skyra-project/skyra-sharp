namespace Skyra.Structures.Base
{
	public abstract class Piece
	{
		protected Piece(string name)
		{
			Name = name;
		}

		public string Name { get; }
	}
}
