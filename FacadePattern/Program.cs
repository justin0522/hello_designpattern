using System;

namespace FacadePattern
{
	// 银行
	class Bank
	{
		// 判断银行是否有足够的金额
		public bool SufficientSavings( Customer c )
		{
			Console.WriteLine("Check bank for {0}", c.Name );
			return true;
		}
	}

	// 存款
	class Credit
	{
		// 判断用户的存款状况是否符合
		public bool GoodCredit( int amount, Customer c )
		{
			Console.WriteLine( "Check credit for {0}", c.Name );
			return true;
		}
	}

	// 贷款
	class Loan
	{
		// 判断用户的贷款状况是否符合
		public bool GoodLoan( Customer c )
		{
			Console.WriteLine( "Check loan for {0}", c.Name );
			return true;
		}
	}

	// 顾客
	class Customer
	{
		private string name;

		public Customer( string name )
		{
			this.name = name;
		}

		public string Name
		{
			get{ return name; }
		}
	}

	// 外观
	class MortgageApplication
	{
		int amount;
		private Bank bank = new Bank();
		private Loan loan = new Loan();
		private Credit credit = new Credit();

		public MortgageApplication( int amount )
		{
			this.amount = amount;
		}

		// 判断用户借款是否符合条件
		public bool IsEligible( Customer c )
		{
			if( !bank.SufficientSavings( c ) ) return false;
			if( !loan.GoodLoan( c ) ) return false;
			if( !credit.GoodCredit( amount, c )) return false;
			return true;
		}
	}

	/// <summary>
	/// FacadePattern 的摘要说明。
	/// </summary>
	public class FacadePattern
	{
		public static void Main(string[] args)
		{
			MortgageApplication mortgage = 
				new MortgageApplication( 125000 );

			// 通过外观来判断贷款条件是否符合
			mortgage.IsEligible( 
				new Customer( "Kate Green" ) );

		}
	}
}
