using System;
using System.Collections;

namespace ObserverPattern
{
	// "Subject" 
	abstract class Stock
	{
		protected string symbol;  // 股票标记
		protected double price;   // 股票价格
		private ArrayList investors = new ArrayList();

		public Stock( string symbol, double price )
		{
			this.symbol = symbol;
			this.price = price;
		}

		// 添加投资者
		public void Attach( Investor investor )
		{
			investors.Add( investor );
		}

		// 删除投资者
		public void Detach( Investor investor )
		{
			investors.Remove( investor );
		}

		public void Notify()
		{
			foreach( Investor i in investors )
				i.Update( this );
		}

		public double Price
		{
			get{ return price; }
			set
			{
				price = value;
				Notify(); }
		}

		public string Symbol
		{
			get{ return symbol; }
			set{ symbol = value; }
		}
	}

	// "ConcreteSubject"
	class IBM : Stock
	{
		public IBM( string symbol, double price )
			: base( symbol, price )
		{}
	}

	// "Observer"
	interface IInvestor
	{
		void Update( Stock stock );
	}

	// "ConcreteObserver"
	class Investor : IInvestor
	{
		private string name;
		private string observerState;
		private Stock stock;

		public Investor( string name )
		{
			this.name = name;
		}

		// 通知投资者股票价格已改变
		public void Update( Stock stock )
		{
			Console.WriteLine( "Notified investor {0} of {1}'s " +
				"change to {2:C}", name, stock.Symbol, stock.Price );
		}

		public Stock Stock
		{
			get{ return stock; }
			set{ stock = value; }
		}
	}

	/// <summary>
	/// ObserverPattern 的摘要说明。
	/// </summary>
	public class ObserverPattern
	{
		public static void Main( string[] args )
		{
			// 创建投资者对象
			Investor m = new Investor( "Mike" );
			Investor b = new Investor( "Bob" );

			// 创建 IBM stock对象并添加投资者
			IBM ibm = new IBM( "IBM", 120.00 );
			ibm.Attach( m );
			ibm.Attach( b );

			// 改变股票价格，将会自动通知投资者
			ibm.Price = 120.10;
			ibm.Price = 121.00;
			ibm.Price = 120.50;
			ibm.Price = 120.75;
		}
	}
}
