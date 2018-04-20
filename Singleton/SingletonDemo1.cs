using System;

namespace Singleton
{	
	class SingletonDemo1
	{
		private static SingletonDemo1 theSingleton = null; 
		private SingletonDemo1() {} 

		public static SingletonDemo1 Instance() 
		{
			if(null == theSingleton) 
			{ 
				theSingleton = new SingletonDemo1(); 
			} 
			return theSingleton; 
		} 

		public	static void Test() 
		{
			SingletonDemo1 s1 = SingletonDemo1.Instance(); 
			SingletonDemo1 s2 = SingletonDemo1.Instance(); 
			if (s1.Equals(s2)) 
			{
				Console.WriteLine("see, only one instance!"); 
			} 
		} 
	} 
}
