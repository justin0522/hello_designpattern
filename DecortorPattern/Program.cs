using System;
using System.Collections;

namespace DecortorPattern
{
    // "Component" 图书馆的项
    abstract class LibraryItem
    {
        // 项的数目
        private int numCopies;

        public int NumCopies
        {
            get { return numCopies; }
            set { numCopies = value; }
        }

        public abstract void Display();
    }

    // "ConcreteComponent"  图书类
    class Book : LibraryItem
    {
        private string author;  // 图书作者
        private string title;   // 书名

        public Book(string author, string title, int numCopies)
        {
            this.author = author;
            this.title = title;
            this.NumCopies = numCopies;
        }

        // 显示图书类的信息
        public override void Display()
        {
            Console.WriteLine("\nBook ------ ");
            Console.WriteLine(" Author: {0}", author);
            Console.WriteLine(" Title: {0}", title);
            Console.WriteLine(" # Copies: {0}", NumCopies);
        }
    }

    // "ConcreteComponent" 光盘类
    class Video : LibraryItem
    {
        private string director;  // 导演
        private string title;     // 名称
        private int playTime;     // 播放时间

        public Video(string director, string title,
            int numCopies, int playTime)
        {
            this.director = director;
            this.title = title;
            this.NumCopies = numCopies;
            this.playTime = playTime;
        }

        // 显示关盘类的信息
        public override void Display()
        {
            Console.WriteLine("\nVideo ----- ");
            Console.WriteLine(" Director: {0}", director);
            Console.WriteLine(" Title: {0}", title);
            Console.WriteLine(" # Copies: {0}", NumCopies);
            Console.WriteLine(" Playtime: {0}", playTime);
        }
    }

    // "Decorator"
    abstract class Decorator : LibraryItem
    {
        // 定义一个与抽象构件一致的接口
        protected LibraryItem libraryItem;

        public Decorator(LibraryItem libraryItem)
        {
            this.libraryItem = libraryItem;
        }

        public override void Display()
        {
            libraryItem.Display();
        }
    }

    // "ConcreteDecorator"
    class Borrowable : Decorator
    {
        // 附加的属性，借阅者
        protected ArrayList borrowers = new ArrayList();

        public Borrowable(LibraryItem libraryItem)
            : base(libraryItem) { }

        // 借出
        public void BorrowItem(string name)
        {
            borrowers.Add(name);
            libraryItem.NumCopies--;
        }

        // 归还
        public void ReturnItem(string name)
        {
            borrowers.Remove(name);
            libraryItem.NumCopies++;
        }

        public override void Display()
        {
            base.Display();
            // 输出借阅者的信息
            foreach (string borrower in borrowers)
                Console.WriteLine(" borrower: {0}", borrower);
        }
    }

    /// <summary>
    /// DecoratorPattern 的摘要说明。
    /// </summary>
    public class DecoratorPattern
    {
        public static void Main(string[] args)
        {
            // 创建书和关盘对象并显示对象信息
            Book book = new Book("Schnell", "My Home", 10);
            Video video = new Video("Spielberg", "Schindler's list", 23, 60);

            book.Display();
            video.Display();

            // 借出关盘并显示借出信息
            Console.WriteLine("\nVideo made borrowable:");
            Borrowable borrowvideo = new Borrowable(video);
            borrowvideo.BorrowItem("Cindy Lopez");
            borrowvideo.BorrowItem("Samuel King");

            borrowvideo.Display();
        }
    }
}
