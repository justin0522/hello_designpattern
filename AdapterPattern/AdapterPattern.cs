using System;

namespace AdapterPattern
{
	/// <summary>
	/// AdapterPattern 的摘要说明。
	/// </summary>
	public class AdapterPattern
	{
		public interface Target
		{
			void sampleOperation1();
			void sampleOperation2(); //源类不包含的方法
		}

		public class Adaptee
		{
			public void sampleOperation1()
			{
			}
		}

		public class Adapter: Adaptee,Target
		{
			public void sampleOperation2()
			{

			}
		}
	}
}
