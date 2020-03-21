namespace Skyra.Core.Cache.Models
{
	public interface ICoreBaseStructure<T> where T : class
	{
		public IClient Client { get; }
		public T Patch(T value);
		public T Clone();
	}
}
