using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 定义：策略模式是针对一组算法，将每个算法封装到具有公共接口的独立的类中，从而使它们可以相互替换。
 优点：
    •策略类之间可以自由切换。由于策略类都实现同一个接口，所以使它们之间可以自由切换。
    •易于扩展。增加一个新的策略只需要添加一个具体的策略类即可，基本不需要改变原有的代码。
    •避免使用多重条件选择语句，充分体现面向对象设计思想。
 缺点：
    •客户端必须知道所有的策略类，并自行决定使用哪一个策略类。
    •策略模式会造成很多的策略类。
 适用场景：
    •一个系统需要动态地在几种算法中选择一种的情况下。那么这些算法可以包装到一个个具体的算法类里面，并为这些具体的算法类提供一个统一的接口。
    •如果一个对象有很多的行为，如果不使用合适的模式，这些行为就只好使用多重的if-else语句来实现，此时，可以使用策略模式，把这些行为转移到相应的具体策略类里面，就可以避免使用难以维护的多重条件选择语句，并体现面向对象涉及的概念。
 */

namespace 策略模式
{
    class Program
    {
        static void Main(string[] args)
        {
            // 个人所得税方式
            InterestOperation operation = new InterestOperation(new PersonalTaxStrategy());
            Console.WriteLine("个人支付的税为：{0}", operation.GetTax(5000.00));

            // 企业所得税
            operation = new InterestOperation(new EnterpriseTaxStrategy());
            Console.WriteLine("企业支付的税为：{0}", operation.GetTax(50000.00));

            Console.Read();
        }
    }

    // 所得税计算策略
    public interface ITaxStragety
    {
        double CalculateTax(double income);
    }

    // 个人所得税
    public class PersonalTaxStrategy : ITaxStragety
    {
        public double CalculateTax(double income)
        {
            return income * 0.12;
        }
    }

    // 企业所得税
    public class EnterpriseTaxStrategy : ITaxStragety
    {
        public double CalculateTax(double income)
        {
            return (income - 3500) > 0 ? (income - 3500) * 0.045 : 0.0;
        }
    }

    public class InterestOperation
    {
        private ITaxStragety m_strategy;
        public InterestOperation(ITaxStragety strategy)
        {
            this.m_strategy = strategy;
        }

        public double GetTax(double income)
        {
            return m_strategy.CalculateTax(income);
        }
    }

}
