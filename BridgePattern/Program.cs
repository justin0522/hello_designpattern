using System;
using System.Collections;

namespace BridgePattern
{
	/// <summary>
	/// BridgePattern 的摘要说明。
	/// </summary>
	public class BridgePattern
	{
		// "Abstraction"
		class BusinessObject
		{
			// 字段
			private DataObject dataObject;
			protected string group;

			// 构造器
			public BusinessObject( string group )
			{
				this.group = group;
			}

			// 属性
			public DataObject DataObject
			{
				set{ dataObject = value; }
				get{ return dataObject; }
			}

			// 方法
			virtual public void Next()
			{
				dataObject.NextRecord();
			}

			virtual public void Prior()
			{
				dataObject.PriorRecord();
			}

			virtual public void New( string name )
			{
				dataObject.NewRecord( name );
			}

			virtual public void Delete( string name )
			{
				dataObject.DeleteRecord( name );
			}

			virtual public void Show()
			{
				dataObject.ShowRecord();
			}

			virtual public void ShowAll()
			{
				Console.WriteLine( "Customer Group: {0}", group );
				dataObject.ShowAllRecords();
			}
		}

		// "RefinedAbstraction"
		class CustomersBusinessObject: BusinessObject
		{
			// 构造器
			public CustomersBusinessObject( string group ) 
				: base( group ){}

			// 方法
			override public void ShowAll()
			{
				Console.WriteLine();
				Console.WriteLine( "------------------------" );
				base.ShowAll();
				Console.WriteLine( "------------------------" );
			}
		}

		// "Implementor"
		abstract class DataObject
		{
			// 方法
			abstract public void NextRecord();
			abstract public void PriorRecord();
			abstract public void NewRecord( string name );
			abstract public void DeleteRecord( string name );
			abstract public void ShowRecord();
			abstract public void ShowAllRecords();
		}

		// "ConcreteImplementor" 
		class CustomersDataObject: DataObject
		{
			// 字段
			private ArrayList customers = new ArrayList();
			private int current = 0;

			// 构造器
			public CustomersDataObject() 
			{
				customers.Add( "Jim Jones" );
				customers.Add( "Samual Jackson" );
				customers.Add( "Allen Good" );
				customers.Add( "Ann Stills" );
				customers.Add( "Lisa Giolani" );
			}

			// 后一个记录
			public override void NextRecord()
			{
				if( current <= customers.Count - 1 )
					current++;
			}

			// 前一个记录
			public override void PriorRecord()
			{
				if( current > 0 )
					current--;
			}

			// 添加记录
			public override void NewRecord( string name )
			{
				customers.Add( name );
			}

			// 删除记录
			public override void DeleteRecord( string name )
			{
				customers.Remove( name );
			}

			// 显示记录
			public override void ShowRecord()
			{
				Console.WriteLine( customers[ current ] );
			}

			// 显示所有记录
			public override void ShowAllRecords()
			{
				foreach( string name in customers )
					Console.WriteLine( " " + name );
			}
		}

		/// <summary>
		///    客户端测试
		/// </summary>
		public class BusinessApp
		{
			public static void Main( string[] args )
			{     	
				// 创建 RefinedAbstraction
				BusinessObject customers = 
					new CustomersBusinessObject(" Chicago ");

				// 设置 ConcreteImplementor
				customers.DataObject = new CustomersDataObject();

				// 测试桥接的效果
				customers.Show();
				customers.Next();
				customers.Show();
				customers.Next();
				customers.Show();
				customers.New( "Henry Velasquez" );

				customers.ShowAll();

			}
		}
	}
}
