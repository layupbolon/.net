using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 中介者模式，定义了一个中介对象来封装系列对象之间的交互。中介者使各个对象不需要显式地相互引用，从而使其耦合性降低，而且可以独立地改变它们之间的交互。中介者模式一般应用于一组定义良好的对象之间需要进行通信的场合以及想定制一个分布在多个类中的行为，而又不想生成太多的子类的情形下。
 * 
 *中介者模式的适用场景：
 *  一组定义良好的对象，现在要进行复杂的相互通信。
 *  想通过一个中间类来封装多个类中的行为，而又不想生成太多的子类。
 *优点：
 *  简化了对象之间的关系，将系统的各个对象之间的相互关系进行封装，将各个同事类解耦，使得系统变为松耦合。
 *  提供系统的灵活性，使得各个同事对象独立而易于复用。
 *缺点：
 *  中介者模式中，中介者角色承担了较多的责任，所以一旦这个中介者对象出现了问题，整个系统将会受到重大的影响。例如，QQ游戏中计算欢乐豆的程序出错了，这样会造成重大的影响。
 *  新增加一个同事类时，不得不去修改抽象中介者类和具体中介者类，此时可以使用观察者模式和状态模式来解决这个问题。
*/

namespace 中介者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractCardPartner A = new CardPartnerA();
            A.MoneyCount = 20;//初始化牌友的钱
            AbstractCardPartner B = new CardPartnerB(20);

            Mediator mediator = new Mediator(A, B);

            mediator.AWin(6);//A赢了6
            mediator.BWin(10);

            Console.WriteLine(A.MoneyCount.ToString());
            Console.WriteLine(B.MoneyCount.ToString());
            Console.ReadKey();
        }

        /// <summary>
        /// 抽象牌友类
        /// </summary>
        public abstract class AbstractCardPartner
        {
            private int moneyCount = 0;
            public int MoneyCount
            {
                get { return moneyCount; }
                set { moneyCount = value; }
            }

            public AbstractCardPartner()
            {
                moneyCount = 0;
            }

            public AbstractCardPartner(int _moneyCout)
            {
                moneyCount = _moneyCout;
            }

            public abstract void MoneyChang(int count, AbstractMediator mediator);
        }

        /// <summary>
        /// 具体牌友A
        /// </summary>
        public class CardPartnerA : AbstractCardPartner
        {
            public CardPartnerA() { }
            public CardPartnerA(int moneyCout) : base(moneyCout) { }

            public override void MoneyChang(int count, AbstractMediator mediator)
            {
                mediator.AWin(count);
            }
        }

        /// <summary>
        /// 具体牌友B
        /// </summary>
        public class CardPartnerB : AbstractCardPartner
        {
            public CardPartnerB() { }
            public CardPartnerB(int moneyCout) : base(moneyCout) { }

            public override void MoneyChang(int count, AbstractMediator mediator)
            {
                mediator.BWin(count);
            }
        }

        /// <summary>
        /// 抽象中介者
        /// </summary>
        public abstract class AbstractMediator
        {
            protected AbstractCardPartner PlayerA;
            protected AbstractCardPartner PlayerB;

            public AbstractMediator(AbstractCardPartner a, AbstractCardPartner b)
            {
                PlayerA = a;
                PlayerB = b;
            }

            public abstract void AWin(int count);
            public abstract void BWin(int count);
        }

        /// <summary>
        /// 具体中介者
        /// 在具体中介者中实现同事之间的通讯
        /// </summary>
        public class Mediator : AbstractMediator
        {
            public Mediator(AbstractCardPartner a, AbstractCardPartner b)
                : base(a, b)
            {

            }

            public override void AWin(int count)
            {
                PlayerA.MoneyCount += count;
                PlayerB.MoneyCount -= count;
            }

            public override void BWin(int count)
            {
                PlayerB.MoneyCount += count;
                PlayerA.MoneyCount -= count;
            }
        }
    }
}
