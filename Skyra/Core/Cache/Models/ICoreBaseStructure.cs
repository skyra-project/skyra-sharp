namespace Skyra.Core.Cache.Models
{
	public interface ICoreBaseStructure<T> where T : class
	{
		public void Patch(T value);
		public T Clone();
	}
}
