using System;
using System.Collections;

namespace CompositePattern
{

	// 部件
	abstract class DrawingElement
	{
		protected string name;

		public DrawingElement( string name )
		{
			this.name = name;
		}
  
		abstract public void Add(DrawingElement d);
		abstract public void Remove( DrawingElement d );
		abstract public void Display( int indent );
	}

	// 叶子
	class PrimitiveElement : DrawingElement
	{
		public PrimitiveElement( string name ) : base( name ) {}

		public override void Add( DrawingElement c )
		{
			Console.WriteLine("Cannot add to a PrimitiveElement");
		}
		public override void Remove( DrawingElement c )
		{
			Console.WriteLine("Cannot remove from a PrimitiveElement");
		}
		public override void Display( int indent )
		{
			Console.WriteLine( new String( '-', indent ) + " draw a {0}", name );
		}
	}

	// 组合
	class CompositeElement : DrawingElement
	{
		private ArrayList elements = new ArrayList();
	
		public CompositeElement( string name ) 
			: base( name ) {}

		public override void Add( DrawingElement d )
		{
			elements.Add( d );
		}

		public override void Remove( DrawingElement d )
		{
			elements.Remove( d );
		}

		public override void Display( int indent )
		{
			Console.WriteLine( new String( '-', indent ) + 
				"+ " + name );

			// 显示该节点的所有孩子信息
			foreach( DrawingElement c in elements )
				c.Display( indent + 2 ); // 递归调用
		}
	}

	/// <summary>
	/// CompositePattern 的摘要说明。
	/// </summary>
	public class CompositePattern
	{
		public static void Main( string[] args )
		{   
			// 创建一个树结构 
			CompositeElement root = new CompositeElement( "Picture" );
			root.Add( new PrimitiveElement( "Red Line" ));
			root.Add( new PrimitiveElement( "Blue Circle" ));
			root.Add( new PrimitiveElement( "Green Box" ));

			// 往树根添加一个组合
			CompositeElement comp = new CompositeElement( "Two Circles" );
			comp.Add( new PrimitiveElement( "Black Circle" ) );
			comp.Add( new PrimitiveElement( "White Circle" ) );
			root.Add( comp );

			// 添加和移除叶子
			PrimitiveElement l = new PrimitiveElement( "Yellow Line" );
			root.Add( l );
			root.Remove( l );

			// 显示节点信息
			root.Display( 1 );
		}
	}
}
