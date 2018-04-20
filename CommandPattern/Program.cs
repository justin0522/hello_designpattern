using System;
using System.Collections;

namespace CommandPattern
{
	// "Command"
	abstract class Command
	{
		abstract public void Execute();
		abstract public void UnExecute();
	}

	// "ConcreteCommand"
	class CalculatorCommand : Command
	{
		char @operator;   // 运算符
		int operand;      // 操作数
		Calculator calculator; 

		public CalculatorCommand( Calculator calculator, 
			char @operator, int operand )
		{
			this.calculator = calculator;
			this.@operator = @operator;
			this.operand = operand;
		}

		// 属性
		public char Operator
		{
			set{ @operator = value; }
		}

		public int Operand
		{
			set{ operand = value; }
		}

		// 执行
		override public void Execute()
		{
			calculator.Operation( @operator, operand );
		}
  
		// 撤销
		override public void UnExecute()
		{
			calculator.Operation( Undo( @operator ), operand );
		}

		// 撤销操作的帮助函数（返回相反的运算符）
		private char Undo( char @operator )
		{
			char undo = ' ';
			switch( @operator )
			{
				case '+': undo = '-'; break;
				case '-': undo = '+'; break;
				case '*': undo = '/'; break;
				case '/': undo = '*'; break;
			}
			return undo;
		}
	}

	// "Receiver"
	class Calculator
	{
		private int total = 0;  // 计算结果

		public void Operation( char @operator, int operand )
		{
			switch( @operator )
			{
				case '+': total += operand; break;
				case '-': total -= operand; break;
				case '*': total *= operand; break;
				case '/': total /= operand; break;
			}
			Console.WriteLine( "Total = {0} (following {1} {2})", 
				total, @operator, operand );
		}
	}

	// "Invoker"
	class User
	{
		private Calculator calculator = new Calculator();
		private ArrayList commands = new ArrayList();
		private int current = 0;

		public void Redo( int levels )
		{
			Console.WriteLine( "---- Redo {0} levels ", levels );
			// 进行重复操作
			for( int i = 0; i < levels; i++ )
				if( current < commands.Count - 1 )
					((Command)commands[ current++ ]).Execute();
		}

		public void Undo( int levels )
		{
			Console.WriteLine( "---- Undo {0} levels ", levels );
			// 进行撤销操作
			for( int i = 0; i < levels; i++ )
				if( current > 0 )
					((Command)commands[ --current ]).UnExecute();
		}

		public void Compute( char @operator, int operand )
		{
			// 创建命令并执行之
			Command command = new CalculatorCommand( 
				calculator, @operator, operand );
			command.Execute();

			// 将命令添加至ArrayList保存
			commands.Add( command );
			current++;
		}
	}

	/// <summary>
	/// CommandPattern 的摘要说明。Client角色
	/// </summary>
	public class CommandPattern
	{
		public static void Main( string[] args )
		{
			// 创建用户并执行计算操作
			User user = new User();

			user.Compute( '+', 100 );
			user.Compute( '-', 50 );
			user.Compute( '*', 10 );
			user.Compute( '/', 2 );

			// 撤销和重复操作
			user.Undo( 4 );
			user.Redo( 3 );
		}
	}
}
