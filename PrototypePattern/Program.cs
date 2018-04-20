using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Data;

namespace PrototypePattern
{
    /// <summary>
    /// PrototypePattern 的摘要说明。
    /// </summary>
    public class PrototypePattern
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            //定义原型管理器
            NoodleManager noodleManager = new NoodleManager();

            //客户要求下面三碗面
            Noodle beefNoodle = (Noodle)noodleManager["牛肉拉面"].Clone();
            //Noodle beefNoodle=(Noodle)noodleManager["牛肉拉面"].DeepClone();
            Noodle muttonNoodle = (Noodle)noodleManager["羊肉拉面"].Clone();
            Noodle beefCutNoodle = (Noodle)noodleManager["牛肉刀削面"].Clone();

            //修改克隆对象中的引用对象的属性，验证它是浅拷贝还是深拷贝
            beefNoodle.TbName = " 克隆对象已改名";

            //显示原始对象的NoodelName和TbName 
            Console.WriteLine(noodleManager["牛肉拉面"].NoodleName
                + noodleManager["牛肉拉面"].TbName + "\n");
            //显示克隆对象的NoodleName和TbName
            Console.WriteLine(beefNoodle.NoodleName + beefNoodle.TbName + "\n");


            // 将新的产品加入原型管理器，以备以后克隆时使用，
            // 下面是定义了一种新的面条－羊肉刀削面，
            // 并把它添加到面条管理器中，如果以后再有客户点这个面，直接克隆即可。
            noodleManager["羊肉刀削面"] = new CutNoodle("羊肉刀削面");

            //克隆一碗羊肉刀削面
            Noodle muttonCutNoodle = (Noodle)noodleManager["羊肉刀削面"].Clone();
            Console.WriteLine(noodleManager["羊肉刀削面"].NoodleName + "\n");
            Console.WriteLine(muttonCutNoodle.NoodleName + "\n");
            Console.ReadLine();
        }
    }

    //抽象产品－面条
    //序列化属性，为深拷贝时使用，每个派生类都要加上此属性才能实现深拷贝
    [Serializable]
    public abstract class Noodle
    {
        //定义一个DataTable对象，主要是为了验证对比类中含有引用对象时的深
        //拷贝和浅拷贝时的不同，也可以采用别的任何引用对象   
        protected DataTable dataTable = new DataTable();
        public string TbName
        {
            get { return dataTable.TableName; }
            set { dataTable.TableName = value; }
        }

        //字段
        protected string noodleName;
        //特性
        public string NoodleName
        {
            get { return noodleName; }
            set { noodleName = value; }
        }

        public abstract Noodle Make(string name);
        //浅克隆的接口
        public abstract Noodle Clone();
        //深克隆的接口
        public abstract Noodle DeepClone();
    }

    //具体产品，拉面
    [Serializable]
    public class PullNoodle : Noodle
    {

        public PullNoodle(string name)
        {
            this.NoodleName = name;
            this.TbName = name + "table";
            Console.WriteLine("PullNoodle is made\n");
        }

        //实现浅拷贝
        public override Noodle Clone()
        {
            return (Noodle)this.MemberwiseClone();
        }

        //实现深拷贝，先将对象序列化到内存流，再反序列化，即可得到深克隆
        public override Noodle DeepClone()
        {
            //定义内存流
            MemoryStream ms = new MemoryStream();
            //定义二进制流
            IFormatter bf = new BinaryFormatter();
            //序列化
            bf.Serialize(ms, this);
            //重置指针到起始位置，以备反序列化
            ms.Position = 0;
            //返回反序列化的深克隆对象
            return (Noodle)bf.Deserialize(ms);
        }

        public override Noodle Make(string name)
        {
            return new PullNoodle(name);
        }

    }

    //具体产品－刀削面
    [Serializable]
    public class CutNoodle : Noodle
    {
        public CutNoodle(string name)
        {
            this.NoodleName = name;
            this.TbName = name + "table";
            Console.WriteLine("CutNoodle is made\n");
        }

        //实现浅克隆
        public override Noodle Clone()
        {
            return (Noodle)this.MemberwiseClone();
        }

        public override Noodle Make(string name)
        {
            return new CutNoodle(name);
        }

        //实现深克隆
        public override Noodle DeepClone()
        {
            MemoryStream ms = new MemoryStream();
            IFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            ms.Position = 0;
            return (Noodle)bf.Deserialize(ms);
        }
    }

    //定义原型管理器，用于存储原型集合，采用HashTable
    class NoodleManager
    {
        //定义HashTable
        protected Hashtable noodleHt = new Hashtable();
        protected Noodle noodle;

        public NoodleManager()
        {

            //初始化时加入三种基本原型
            noodle = new PullNoodle("牛肉拉面");
            noodleHt.Add("牛肉拉面", noodle);
            noodle = new PullNoodle("羊肉拉面");
            noodleHt.Add("羊肉拉面", noodle);
            noodle = new CutNoodle("牛肉刀削面");
            noodleHt.Add("牛肉刀削面", noodle);

        }
        //索引器，用于添加，访问Noodle对象
        public Noodle this[string key]
        {
            get { return (Noodle)noodleHt[key]; }
            set { noodleHt.Add(key, value); }
        }
    }
}
