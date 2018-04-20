using System;

namespace ProxyPattern
{
    public interface ISubject
    {
        void Request();
    }

    public class SubjectA : ISubject
    {
        public void Request()
        {
            Console.WriteLine("Subject A");
        }
    }

    public class SubjectB : ISubject
    {
        public void Request()
        {
            Console.WriteLine("Subject B");
        }
    }

    public class Proxy:ISubject
    {
        ISubject subject;
        public Proxy(string s)
        {
                if(s == "A")
                {
                    subject = new SubjectA();
                }
                else
                {
                    subject = new SubjectB();
                }
        }

        // public void SetSubject()
        // {

        // }

        public void Request()
        {
            subject.Request();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Proxy proxy = new Proxy("A");
            proxy.Request();
        }
    }
}
