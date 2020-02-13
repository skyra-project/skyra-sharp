namespace Skyra.Models.Gateway.Base
{
	public struct GenericEventArgs<T>
	{
		public readonly T Data;

		public GenericEventArgs(T data)
		{
			Data = data;
		}
	}
}
