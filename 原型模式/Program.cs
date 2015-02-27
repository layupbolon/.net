using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 当我们需要多个相同的类实例时，没必要每次都使用new运算符去创建相同的类实例对象，此时我们一般思路就是想——只创建一个类实例对象，如果后面需要更多这样的实例，可以通过对原来对象拷贝一份来完成创建，这样在内存中不需要创建多个相同的类实例，从而减少内存的消耗和达到类实例的复用。
 */

namespace 原型模式
{
    class Program
    {
        static void Main(string[] args)
        {
            // 孙悟空 原型
            MonkeyKingPrototype prototypeMonkeyKing = new ConcretePrototype("MonkeyKing");

            // 变一个
            MonkeyKingPrototype cloneMonkeyKing = prototypeMonkeyKing.Clone() as ConcretePrototype;
            Console.WriteLine("Cloned1:\t" + cloneMonkeyKing.Id);

            // 变两个
            MonkeyKingPrototype cloneMonkeyKing2 = prototypeMonkeyKing.Clone() as ConcretePrototype;
            Console.WriteLine("Cloned2:\t" + cloneMonkeyKing2.Id);
            Console.ReadLine();

        }
    }

    /// <summary>
    /// 孙悟空原型
    /// </summary>
    public abstract class MonkeyKingPrototype
    {
        public string Id { get; set; }
        public MonkeyKingPrototype(string id)
        {
            this.Id = id;
        }

        // 克隆方法，即孙大圣说“变”
        public abstract MonkeyKingPrototype Clone();
    }

    /// <summary>
    /// 创建具体原型
    /// </summary>
    public class ConcretePrototype : MonkeyKingPrototype
    {
        public ConcretePrototype(string id)
            : base(id)
        { }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns></returns>
        public override MonkeyKingPrototype Clone()
        {
            // 调用MemberwiseClone方法实现的是浅拷贝，另外还有深拷贝
            return (MonkeyKingPrototype)this.MemberwiseClone();
        }
    }

}
