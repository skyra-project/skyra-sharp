namespace Skyra.Structures.Base
{
	public abstract class Piece
	{
		public string Name { get; }

		protected Piece(string name)
		{
			Name = name;
		}
	}
}
