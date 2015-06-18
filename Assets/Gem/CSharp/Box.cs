namespace SPRPG
{
	public class Box<T>
	{
		public T Value;
		
		public Box()
		{}

		public Box(T value)
		{
			Value = value;
		}
	}
}