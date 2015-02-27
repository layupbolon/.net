using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 定义:提供一个创建产品的接口来负责创建相关或依赖的对象，而不具体明确指定具体类。
 优点：抽象工厂对于系列产品的变化支持 “开放——封闭”原则（指的是要求系统对扩展开放，对修改封闭），扩展起来非常简便。
 缺点：抽象工厂对于添加新产品这种情况就不支持”开放——封闭 “原则。
 应用场景：
    •一个系统不要求依赖产品类实例如何被创建、组合和表达的表达，这点也是所有工厂模式应用的前提。
    •这个系统有多个系列产品，而系统中只消费其中某一系列产品
    •系统要求提供一个产品类的库，所有产品以同样的接口出现，客户端不需要依赖具体实现。
 */

namespace 抽象工厂模式
{
    class Program
    {
        static void Main(string[] args)
        {
            // 南昌工厂制作南昌的鸭脖和鸭架
            AbstractFactory nanChangFactory = new NanChangFactory();
            YaBo nanChangYabo = nanChangFactory.CreateYaBo();
            nanChangYabo.Print();
            YaJia nanChangYajia = nanChangFactory.CreateYaJia();
            nanChangYajia.Print();

            // 上海工厂制作上海的鸭脖和鸭架
            AbstractFactory shangHaiFactory = new ShangHaiFactory();
            shangHaiFactory.CreateYaBo().Print();
            shangHaiFactory.CreateYaJia().Print();

            Console.Read();

        }
    }

    /// <summary>
    /// 抽象工厂类，提供创建两个不同地方的鸭架和鸭脖的接口
    /// </summary>
    public abstract class AbstractFactory
    {
        // 抽象工厂提供创建一系列产品的接口，这里作为例子，只给出了绝味中鸭脖和鸭架的创建接口
        public abstract YaBo CreateYaBo();
        public abstract YaJia CreateYaJia();
    }

    /// <summary>
    /// 南昌绝味工厂负责制作南昌的鸭脖和鸭架
    /// </summary>
    public class NanChangFactory : AbstractFactory
    {
        // 制作南昌鸭脖
        public override YaBo CreateYaBo()
        {
            return new NanChangYaBo();
        }
        // 制作南昌鸭架
        public override YaJia CreateYaJia()
        {
            return new NanChangYaJia();
        }
    }

    /// <summary>
    /// 上海绝味工厂负责制作上海的鸭脖和鸭架
    /// </summary>
    public class ShangHaiFactory : AbstractFactory
    {
        // 制作上海鸭脖
        public override YaBo CreateYaBo()
        {
            return new ShangHaiYaBo();
        }
        // 制作上海鸭架
        public override YaJia CreateYaJia()
        {
            return new ShangHaiYaJia();
        }
    }

    /// <summary>
    /// 鸭脖抽象类，供每个地方的鸭脖类继承
    /// </summary>
    public abstract class YaBo
    {
        /// <summary>
        /// 打印方法，用于输出信息
        /// </summary>
        public abstract void Print();
    }

    /// <summary>
    /// 鸭架抽象类，供每个地方的鸭架类继承
    /// </summary>
    public abstract class YaJia
    {
        /// <summary>
        /// 打印方法，用于输出信息
        /// </summary>
        public abstract void Print();
    }

    /// <summary>
    /// 南昌的鸭脖类，因为江西人喜欢吃辣的，所以南昌的鸭脖稍微会比上海做的辣
    /// </summary>
    public class NanChangYaBo : YaBo
    {
        public override void Print()
        {
            Console.WriteLine("南昌的鸭脖");
        }
    }

    /// <summary>
    /// 上海的鸭脖没有南昌的鸭脖做的辣
    /// </summary>
    public class ShangHaiYaBo : YaBo
    {
        public override void Print()
        {
            Console.WriteLine("上海的鸭脖");
        }
    }

    /// <summary>
    /// 南昌的鸭架
    /// </summary>
    public class NanChangYaJia : YaJia
    {
        public override void Print()
        {
            Console.WriteLine("南昌的鸭架子");
        }
    }

    /// <summary>
    /// 上海的鸭架
    /// </summary>
    public class ShangHaiYaJia : YaJia
    {
        public override void Print()
        {
            Console.WriteLine("上海的鸭架子");
        }
    }

}
