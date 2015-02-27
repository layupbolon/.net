﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 定义：封装一些施加于某种数据结构之上的操作。一旦这些操作需要修改的话，接受这个操作的数据结构则可以保存不变。
 优点：
    •访问者模式使得添加新的操作变得容易。如果一些操作依赖于一个复杂的结构对象的话，那么一般而言，添加新的操作会变得很复杂。而使用访问者模式，增加新的操作就意味着添加一个新的访问者类。因此，使得添加新的操作变得容易。
    •访问者模式使得有关的行为操作集中到一个访问者对象中，而不是分散到一个个的元素类中。这点类似与”中介者模式”。
    •访问者模式可以访问属于不同的等级结构的成员对象，而迭代只能访问属于同一个等级结构的成员对象。
 缺点：
    •增加新的元素类变得困难。每增加一个新的元素意味着要在抽象访问者角色中增加一个新的抽象操作，并在每一个具体访问者类中添加相应的具体操作。
 适用场景：
    •如果系统有比较稳定的数据结构，而又有易于变化的算法时，此时可以考虑使用访问者模式。因为访问者模式使得算法操作的添加比较容易。
    •如果一组类中，存在着相似的操作，为了避免出现大量重复的代码，可以考虑把重复的操作封装到访问者中。（当然也可以考虑使用抽象类了）
    •如果一个对象存在着一些与本身对象不相干，或关系比较弱的操作时，为了避免操作污染这个对象，则可以考虑把这些操作封装到访问者对象中。
 */

namespace 访问者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectStructure o = new ObjectStructure();
            foreach(Element e in o.Elements)
            {
                e.Accept(new ConcreteVisitor());
            }

            Console.ReadKey();
        }
    }

    public abstract class Element
    {
        public abstract void Accept(IVisitor visitor);
        public abstract void Print();
    }

    public class ElementA : Element
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Print()
        {
            Console.WriteLine("这是A");
        }
    }

    public class ElementB : Element
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Print()
        {
            Console.WriteLine("这是B");
        }
    }

    public interface IVisitor
    {
        void Visit(ElementA a);
        void Visit(ElementB b);
    }

    public class ConcreteVisitor : IVisitor
    {
        public void Visit(ElementA a)
        {
            a.Print();
        }

        public void Visit(ElementB b)
        {
            b.Print();
        }
    }

    public class ObjectStructure
    {
        private List<Element> elements = new List<Element>();
        public List<Element> Elements { get { return this.elements; } }

        public ObjectStructure()
        {
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int num = r.Next(10);
                if (num > 5)
                {
                    elements.Add(new ElementA());
                }
                else
                {
                    elements.Add(new ElementB());
                }
            }
        }
    }
}
