using System;

namespace ChainPattern
{
    // "Handler"
    abstract class Approver
    {
        protected string name;
        protected Approver successor;

        public Approver(string name)
        {
            this.name = name;
        }

        public void SetSuccessor(Approver successor)
        {
            this.successor = successor;
        }

        abstract public void ProcessRequest(
            PurchaseRequest request);
    }

    // "ConcreteHandler"
    class Director : Approver
    {
        public Director(string name) : base(name)
        { }

        override public void ProcessRequest(
            PurchaseRequest request)
        {
            // 请求的资金小于1万的，则通过，否则传递给上级 
            if (request.Amount < 10000.0)
                Console.WriteLine("{0} {1} approved request# {2}",
                    this, name, request.Number);
            else
                if (successor != null)
                successor.ProcessRequest(request);
        }
    }

    // "ConcreteHandler"
    class VicePresident : Approver
    {
        public VicePresident(string name) : base(name) { }
        override public void ProcessRequest(
            PurchaseRequest request)
        {
            // 请求的资金小于2.5万的，则通过，否则传递给上级
            if (request.Amount < 25000.0)
                Console.WriteLine("{0} {1} approved request# {2}",
                    this, name, request.Number);
            else
                if (successor != null)
                successor.ProcessRequest(request);
        }
    }

    // "ConcreteHandler"
    class President : Approver
    {
        public President(string name) : base(name) { }

        override public void ProcessRequest(
            PurchaseRequest request)
        {
            // 请求的资金小于10万的，则通过，否则再议
            if (request.Amount < 100000.0)
                Console.WriteLine("{0} {1} approved request# {2}",
                    this, name, request.Number);
            else
                Console.WriteLine("Request# {0} requires " +
                    "an executive meeting!", request.Number);
        }
    }

    // Request details
    class PurchaseRequest
    {
        private int number;       // 编号
        private double amount;    // 请求资金的数量
        private string purpose;   // 请求资金的目的

        public PurchaseRequest(int number, double amount, string purpose)
        {
            this.number = number;
            this.amount = amount;
            this.purpose = purpose;
        }

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string Purpose
        {
            get { return purpose; }
            set { purpose = value; }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }
    }

    /// <summary>
    /// ChainPattern 的摘要说明。
    /// </summary>
    public class ChainPattern
    {
        public static void Main(string[] args)
        {
            // 建立职责链
            Director Larry = new Director("Larry");
            VicePresident Sam = new VicePresident("Sam");
            President Tammy = new President("Tammy");
            Larry.SetSuccessor(Sam);
            Sam.SetSuccessor(Tammy);

            // 产生并处理不同的请求
            PurchaseRequest rs = new PurchaseRequest(
                1000, 350.00, "Supplies");
            Larry.ProcessRequest(rs);

            PurchaseRequest rx = new PurchaseRequest(
                1001, 32590.10, "Project 1");
            Larry.ProcessRequest(rx);

            PurchaseRequest ry = new PurchaseRequest(
                1002, 122100.00, "Project 2");
            Larry.ProcessRequest(ry);

        }
    }
}
