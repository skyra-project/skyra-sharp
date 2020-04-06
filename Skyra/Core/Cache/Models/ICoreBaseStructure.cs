namespace Skyra.Core.Cache.Models
{
	public interface ICoreBaseStructure<T> where T : class
	{
		public IClient Client { get; set; }
		public T Patch(T value);
		public T Clone();
	}
}
