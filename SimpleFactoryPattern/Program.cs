using System;

namespace SimpleFactoryPattern
{
    /// <summary>
    /// SimpleFactoryPattern 的摘要说明。
    /// </summary>
    public class SimpleFactoryPattern
    {
        //定义Food接口
        public interface Food
        {
            //烹饪
            void Cook();
            //卖出
            void Sell();
        }

        public class Noodle : Food
        {
            private int price;

            public Noodle()
            {
                Console.WriteLine("\nThe Noodle is made..");
            }

            //面条Noodle的Cook方法接口实现
            public void Cook()
            {
                Console.WriteLine("\nNoodle is cooking...");
            }

            //面条Noodle的Sell方法接口实现
            public void Sell()
            {
                Console.WriteLine("\nNoodle has been sold...");
            }

            public int Price
            {
                get { return this.price; }
                set { price = value; }
            }
        }

        public class Rice : Food
        {
            private int price;

            public Rice()
            {
                Console.WriteLine("\nThe Rice is made ..");
            }

            public void Cook()
            {
                Console.WriteLine("\nRice is cooking...");
            }

            public void Sell()
            {
                Console.WriteLine("\nRice has been sold...");
            }

            public int Price
            {
                get { return this.price; }
                set { price = value; }
            }
        }

        public class Bread : Food
        {
            private int price;

            public Bread()
            {
                Console.WriteLine("\nThe Bread is made....");
            }

            public void Cook()
            {
                Console.WriteLine("\nBread is cooking...");
            }
            public void Sell()
            {
                Console.WriteLine("\nBread has been sold...");
            }

            public int Price
            {
                get { return this.price; }
                set { price = value; }
            }
        }


        //定义大厨，他包办这个快餐店里的所有Food,包括面条，面包和米饭
        class Chef
        {
            public static Food MakeFood(string foodName)
            {
                try
                {
                    switch (foodName)
                    {
                        case "noodle": return new Noodle();
                        case "rice": return new Rice();
                        case "bread": return new Bread();
                        default: throw new BadFoodException("Bad food request!");
                    }
                }
                catch (BadFoodException e)
                {
                    throw e;
                }
            }

        }

        //异常类，该餐馆没有的食品
        class BadFoodException : System.Exception
        {
            public BadFoodException(string strMsg)
            {
                Console.WriteLine(strMsg);
            }
        }


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // 根据传入的参数创建Food类的实例
            Food food = Chef.MakeFood("rice");
            food.Cook();
            food.Sell();
            Console.ReadLine();
        }
    }
}
