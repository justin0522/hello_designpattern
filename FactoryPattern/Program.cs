using System;

namespace FactoryPattern
{
	/// <summary>
	/// FactoryPattern 的摘要说明。
	/// </summary>
	public class FactoryPattern
	{

		public interface Creator
		{
			Product factory();//工厂方法
		}

		class ConcreteCreator1:Creator
		{
			public Product factory() //工厂方法
			{
			return new ConcreteProduct1();
			}
		}

		class ConcreteCreator2:Creator
		{
			public Product factory() //工厂方法
			{
				return new ConcreteProduct2();
			}
		}

		public interface Product
		{
         
		}

		class ConcreteProduct1:Product
		{
			public ConcreteProduct1()
			{
				Console.WriteLine ("Creat ConcreteProduct1");
			}
		}

		class ConcreteProduct2:Product
		{
			public ConcreteProduct2()
			{
				Console.WriteLine ("Creat ConcreteProduct2");
			}
		}

		class Client
		{
			private static Creator creator1,creator2;
			private static Product product1,product2;

			[STAThread]
			static void Main(string[] args)
			{
				creator1=new ConcreteCreator1(); 
				product1=creator1.factory();   
                
				creator2=new ConcreteCreator2();
				product2=creator2.factory();
			}
		}
	}
}
