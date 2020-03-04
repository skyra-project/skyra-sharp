namespace Skyra.Core.Cache.Models
{
	public interface ICoreBaseStructure<T> where T : class
	{
		public T Patch(T value);
		public T Clone();
	}
}
