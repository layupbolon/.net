using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 定义：某个请求需要多个对象进行处理，从而避免请求的发送者和接收之间的耦合关系。将这些对象连成一条链子，并沿着这条链子传递该请求，直到有对象处理它为止。
 优点：
    •降低了请求的发送者和接收者之间的耦合。
    •把多个条件判定分散到各个处理类中，使得代码更加清晰，责任更加明确。
 缺点：
    •在找到正确的处理对象之前，所有的条件判定都要执行一遍，当责任链过长时，可能会引起性能的问题
    •可能导致某个请求不被处理。
 适用场景：
    •一个系统的审批需要多个对象才能完成处理的情况下，例如请假系统等。
    •代码中存在多个if-else语句的情况下，此时可以考虑使用责任链模式来对代码进行重构。
 */

namespace 责任链模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Request computer = new Request(15000, "电脑");
            Request house = new Request(100000, "房子");

            AuditPerson manager = new Manager("经理");
            AuditPerson vp = new VP("VP");
            AuditPerson boss = new Boss("老板");

            manager.NextAuditPerson = vp;
            vp.NextAuditPerson = boss;

            manager.Audit(computer);
            manager.Audit(house);

            Console.ReadKey();
        }
    }

    public class Request
    {
        public double Amount { get; set; }
        public string Name { get; set; }

        public Request(double amount, string name)
        {
            this.Amount = amount;
            this.Name = name;
        }
    }

    public abstract class AuditPerson
    {
        public AuditPerson NextAuditPerson { get; set; }
        public string PersonName { get; set; }
        public AuditPerson(string name)
        {
            this.PersonName = name;
        }

        public abstract void Audit(Request request);
    }

    public class Manager : AuditPerson
    {
        public Manager(string name) : base(name) { }

        public override void Audit(Request request)
        {
            if (request.Amount < 10000)
            {
                Console.WriteLine("{0},这个请求可以通过,{1}", request.Name,this.PersonName);
            }
            else
            {
                Console.WriteLine("这个需要领导{0}审核", NextAuditPerson.PersonName);
                NextAuditPerson.Audit(request);
            }
        }
    }

    public class VP : AuditPerson
    {
        public VP(string name) : base(name) { }

        public override void Audit(Request request)
        {
            if (request.Amount <= 50000)
            {
                Console.WriteLine("{0},这个请求可以通过,{1}", request.Name,this.PersonName);
            }
            else
            {
                Console.WriteLine("这个需要领导{0}审核", NextAuditPerson.PersonName);
                NextAuditPerson.Audit(request);
            }
        }
    }

    public class Boss : AuditPerson
    {
        public Boss(string name) : base(name) { }

        public override void Audit(Request request)
        {
            if (request.Amount > 50000)
            {
                Console.WriteLine("这个需要开会讨论");
            }
        }
    }
}
