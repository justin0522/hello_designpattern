using System;

namespace AdapterPattern
{
	/// <summary>
	/// AdapterPattern2 的摘要说明。
	/// </summary>
	public class AdapterPattern2
	{
		public interface Target
		{
			void sampleOperation1();
			void sampleOperation2();
		}

		public class Adaptee
		{
			public void sampleOperation1()
			{
                Console.WriteLine("已存在的功能");
			}
		}

		public class Adapter: Target
		{
			private Adaptee adaptee;

			public Adapter(Adaptee adaptee)
			{
				this.adaptee=adaptee;
			}

			public void sampleOperation1()
			{
                Console.WriteLine("把已存在的功能封装进来");
				adaptee.sampleOperation1();
			}

			public void sampleOperation2()
			{
                Console.WriteLine("新的功能");
			}
		}
	}
}
