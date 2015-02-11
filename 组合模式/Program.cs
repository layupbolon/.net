using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
定义：
组合模式允许你将对象组合成树形结构来表现”部分-整体“的层次结构，使得客户以一致的方式处理单个对象以及对象的组合。下面我们用绘制的例子来详细介绍组合模式，图形可以由一些基本图形元素组成（如直线，圆等），也可以由一些复杂图形组成（由基本图形元素组合而成），为了使客户对基本图形和复杂图形的调用保持一致，我们使用组合模式来达到整个目的。

组合模式实现的最关键的地方是——简单对象和复合对象必须实现相同的接口。这就是组合模式能够将组合对象和简单对象进行一致处理的原因。

优点：
1.组合模式使得客户端代码可以一致地处理对象和对象容器，无需关系处理的单个对象，还是组合的对象容器。
2.将”客户代码与复杂的对象容器结构“解耦。
3.可以更容易地往组合对象中加入新的构件。
缺点：
使得设计更加复杂。客户端需要花更多时间理清类之间的层次关系。（这个是几乎所有设计模式所面临的问题）。
*/


namespace 组合模式
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexGraphics complexGraphics = new ComplexGraphics("一个复杂图形和两条线段组成的复杂图形");
            complexGraphics.Add(new Line("线段A"));
            ComplexGraphics CompositeCG = new ComplexGraphics("一个圆和一条线组成的复杂图形");
            CompositeCG.Add(new Circle("圆"));
            CompositeCG.Add(new Circle("线段B"));
            complexGraphics.Add(CompositeCG);
            Line l = new Line("线段C");
            complexGraphics.Add(l);

            // 显示复杂图形的画法 
            Console.WriteLine("复杂图形的绘制如下：");
            Console.WriteLine("---------------------");
            complexGraphics.Draw();
            Console.WriteLine("复杂图形绘制完成");
            Console.WriteLine("---------------------");
            Console.WriteLine();

            // 移除一个组件再显示复杂图形的画法 
            complexGraphics.Remove(l);
            Console.WriteLine("移除线段C后，复杂图形的绘制如下：");
            Console.WriteLine("---------------------");
            complexGraphics.Draw();
            Console.WriteLine("复杂图形绘制完成");
            Console.WriteLine("---------------------");
            Console.Read(); 

        }
    }

    /// <summary> 
    /// 图形抽象类， 
    /// </summary> 
    public abstract class Graphics
    {
        public string Name { get; set; }
        public Graphics(string name)
        {
            this.Name = name;
        }

        public abstract void Draw();
        // 移除了Add和Remove方法 
        // 把管理子对象的方法放到了ComplexGraphics类中进行管理 
        // 因为这些方法只在复杂图形中才有意义 
    }

    /// <summary> 
    /// 简单图形类——线 
    /// </summary> 
    public class Line : Graphics
    {
        public Line(string name)
            : base(name)
        { }

        // 重写父类抽象方法 
        public override void Draw()
        {
            Console.WriteLine("画  " + Name);
        }
    }

    /// <summary> 
    /// 简单图形类——圆 
    /// </summary> 
    public class Circle : Graphics
    {
        public Circle(string name)
            : base(name)
        { }

        // 重写父类抽象方法 
        public override void Draw()
        {
            Console.WriteLine("画  " + Name);
        }
    }

    /// <summary> 
    /// 复杂图形，由一些简单图形组成,这里假设该复杂图形由一个圆两条线组成的复杂图形 
    /// </summary> 
    public class ComplexGraphics : Graphics
    {
        private List<Graphics> complexGraphicsList = new List<Graphics>();
        public ComplexGraphics(string name)
            : base(name)
        { }

        /// <summary> 
        /// 复杂图形的画法 
        /// </summary> 
        public override void Draw()
        {
            foreach (Graphics g in complexGraphicsList)
            {
                g.Draw();
            }
        }

        public void Add(Graphics g)
        {
            complexGraphicsList.Add(g);
        }
        public void Remove(Graphics g)
        {
            complexGraphicsList.Remove(g);
        }
    }
}
