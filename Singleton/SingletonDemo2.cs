using System;

namespace Singleton
{
	using System; 
   
	class SingletonDemo2
	{
		private static readonly SingletonDemo2 theSingleton = new SingletonDemo2(); 

		private SingletonDemo2() {} 

		public static SingletonDemo2 Instance() 
		{
			return theSingleton; 
		} 
	    public	static void Test() 
		{
			SingletonDemo2 s1 = SingletonDemo2.Instance(); 
			SingletonDemo2 s2 = SingletonDemo2.Instance(); 
			if (s1.Equals(s2)) 
			{
				Console.WriteLine("see, only one instance!"); 
			} 
		} 
	} 
}
