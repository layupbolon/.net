using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 定义：
    观察者模式定义了一种一对多的依赖关系，让多个观察者对象同时监听某一个主题对象，这个主题对象在状态发生变化时，会通知所有观察者对象，使它们能够自动更新自己的行为。
 优点：
    •观察者模式实现了表示层和数据逻辑层的分离，并定义了稳定的更新消息传递机制，并抽象了更新接口，使得可以有各种各样不同的表示层，即观察者。
    •观察者模式在被观察者和观察者之间建立了一个抽象的耦合，被观察者并不知道任何一个具体的观察者，只是保存着抽象观察者的列表，每个具体观察者都符合一个抽象观察者的接口。
    •观察者模式支持广播通信。被观察者会向所有的注册过的观察者发出通知。
 缺点：
    •如果一个被观察者有很多直接和间接的观察者时，将所有的观察者都通知到会花费很多时间。
    •虽然观察者模式可以随时使观察者知道所观察的对象发送了变化，但是观察者模式没有相应的机制使观察者知道所观察的对象是怎样发生变化的。
    •如果在被观察者之间有循环依赖的话，被观察者会触发它们之间进行循环调用，导致系统崩溃，在使用观察者模式应特别注意这点。
 应用场景：
    •当一个抽象模型有两个方面，其中一个方面依赖于另一个方面，将这两者封装在独立的对象中以使它们可以各自独立地改变和复用的情况下。
    •当对一个对象的改变需要同时改变其他对象，而又不知道具体有多少对象有待改变的情况下。
    •当一个对象必须通知其他对象，而又不能假定其他对象是谁的情况下。
*/

namespace 观察者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            TenXunGame tenXunGame = new TenXunGame("腾讯订阅号", "新春快乐！");
            tenXunGame.Add(new Subscriber("张三"));
            tenXunGame.Add(new Subscriber("李四"));

            tenXunGame.Update();

            Console.ReadKey();
        }
    }

    public abstract class TenXun
    {
        private List<IObserver> observers = new List<IObserver>();

        public string Symbol { get; set; }
        public string Info { get; set; }

        public TenXun(string symbol, string info)
        {
            this.Symbol = symbol;
            this.Info = info;
        }

        public virtual void Add(IObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public virtual void Remove(IObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        public void Update()
        {
            observers.ForEach(item =>
            {
                item.ReceiveAndPrint(this);
            });
        }
    }

    public class TenXunGame : TenXun
    {
        public TenXunGame(string symbol, string info) : base(symbol, info) { }
    }

    public interface IObserver
    {
        void ReceiveAndPrint(TenXun tenXun);
    }

    public class Subscriber : IObserver
    {
        private string name;

        public Subscriber(string _name)
        {
            this.name = _name;
        }

        public void ReceiveAndPrint(TenXun tenXun)
        {
            Console.WriteLine("{0}收到了{1}发来的消息：{2}", this.name, tenXun.Symbol, tenXun.Info);
        }
    }
}
