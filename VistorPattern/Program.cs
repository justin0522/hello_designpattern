using System;
using System.Collections;

namespace VistorPattern
{
	// "Visitor"
	abstract class Visitor
	{
		abstract public void Visit( Element element );
	}

	// "ConcreteVisitor1" 
	class IncomeVisitor : Visitor
	{
		public override void Visit( Element element )
		{
			Employee employee = ((Employee)element);
    
			// 提供多10％的工资
			employee.Income *= 1.10;
			Console.WriteLine( "{0}'s new income: {1:C}", 
				employee.Name, employee.Income );
		}
	}

	// "ConcreteVisitor2"
	class VacationVisitor : Visitor
	{
		public override void Visit( Element element )
		{
			Employee employee = ((Employee)element);
			// 提供多三天假期
			employee.VacationDays += 3;
			Console.WriteLine( "{0}'s new vacation days: {1}", 
				employee.Name, employee.VacationDays );
		}
	}

	// "Element" 
	abstract class Element
	{
		abstract public void Accept( Visitor visitor );
	}

	// "ConcreteElement" 雇员类
	class Employee : Element
	{
		string name;  // 雇员名字
		double income;  // 收入
		int vacationDays; // 假期天数

		public Employee( string name, double income, 
			int vacationDays )
		{
			this.name = name;
			this.income = income;
			this.vacationDays = vacationDays;
		}

		public string Name
		{
			get{ return name; }
			set{ name = value; }
		}

		public double Income
		{
			get{ return income; }
			set{ income = value; }
		}

		public int VacationDays
		{
			get{ return vacationDays; }
			set{ vacationDays = value; }
		}

		public override void Accept( Visitor visitor )
		{
			visitor.Visit( this );
		}
	}

	// "ObjectStructure"
	class Employees
	{
		private ArrayList employees = new ArrayList();

		public void Attach( Employee employee )
		{
			employees.Add( employee );
		}

		public void Detach( Employee employee )
		{
			employees.Remove( employee );
		}

		public void Accept( Visitor visitor )
		{
			foreach( Employee e in employees )
				e.Accept( visitor );
		}
	}

	/// <summary>
	/// VistorPattern 的摘要说明。
	/// </summary>
	public class VistorPattern
	{
		public static void Main( string[] args )
		{
			// 建立雇员的集合
			Employees e = new Employees();
			e.Attach( new Employee( "Jim", 40000.0, 14 ) );
			e.Attach( new Employee( "Lily", 50000.0, 16 ) );
			e.Attach( new Employee( "Kate", 100000.0, 21 ) );

			// 创建两个访问者
			IncomeVisitor v1 = new IncomeVisitor();
			VacationVisitor v2 = new VacationVisitor();

			// 雇员集合接受访问
			e.Accept( v1 );
			e.Accept( v2 );

		}
	}
}
