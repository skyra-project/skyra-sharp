namespace Skyra.Core.Cache.Models
{
	public interface IBaseStructure<T> where T : class
	{
		public IClient Client { get; set; }
		public T Patch(T value);
		public T Clone();
	}
}
