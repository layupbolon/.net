using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 定义：命令模式是把一个操作或者行为抽象为一个对象中，通过对命令的抽象化来使得发出命令的责任和执行命令的责任分隔开。命令模式的实现可以提供命令的撤销和恢复功能。
 命令模式角色：
    •客户角色：发出一个具体的命令并确定其接受者。
    •命令角色：声明了一个给所有具体命令类实现的抽象接口
    •具体命令角色：定义了一个接受者和行为的弱耦合，负责调用接受者的相应方法。
    •请求者角色：负责调用命令对象执行命令。
    •接受者角色：负责具体行为的执行。
 优点：
    •命令模式使得新的命令很容易被加入到系统里。
    •可以设计一个命令队列来实现对请求的Undo和Redo操作。
    •可以较容易地将命令写入日志。
    •可以把命令对象聚合在一起，合成为合成命令。合成命令式合成模式的应用。
 缺点：
    •使用命令模式可能会导致系统有过多的具体命令类。这会使得命令模式在这样的系统里变得不实际。
 使用场景：
    1.系统需要支持命令的撤销（undo）。命令对象可以把状态存储起来，等到客户端需要撤销命令所产生的效果时，可以调用undo方法吧命令所产生的效果撤销掉。命令对象还可以提供redo方法，以供客户端在需要时，再重新实现命令效果。
    2.系统需要在不同的时间指定请求、将请求排队。一个命令对象和原先的请求发出者可以有不同的生命周期。意思为：原来请求的发出者可能已经不存在了，而命令对象本身可能仍是活动的。这时命令的接受者可以在本地，也可以在网络的另一个地址。命令对象可以串行地传送到接受者上去。
    3.如果一个系统要将系统中所有的数据消息更新到日志里，以便在系统崩溃时，可以根据日志里读回所有数据的更新命令，重新调用方法来一条一条地执行这些命令，从而恢复系统在崩溃前所做的数据更新。
    4.系统需要使用命令模式作为“CallBack(回调)”在面向对象系统中的替代。Callback即是先将一个方法注册上，然后再以后调用该方法。

 */

namespace 命令模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver r = new Receiver();
            Command c = new Run1000Meter(r);
            Invoke i = new Invoke(c);

            i.ExecuteCommand();
            Console.ReadKey();
        }
    }

    public class Invoke
    {
        private Command command;

        public Invoke(Command _command)
        {
            this.command = _command;
        }

        public void ExecuteCommand()
        {
            command.Action();
        }
    }

    public abstract class Command
    {
        protected Receiver receiver;

        public Command(Receiver _receiver)
        {
            this.receiver = _receiver;
        }

        public abstract void Action();
    }

    public class Run1000Meter : Command
    {
        public Run1000Meter(Receiver _receiver) : base(_receiver) { }

        public override void Action()
        {
            this.receiver.Action();
        }
    }

    public class Receiver
    {
        public void Action()
        {
            Console.WriteLine("跑一千米");
        }
    }
}
