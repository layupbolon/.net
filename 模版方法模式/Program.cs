using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 定义：
    在一个抽象类中定义一个操作中的算法骨架（对应于生活中的大家下载的模板），而将一些步骤延迟到子类中去实现（对应于我们根据自己的情况向模板填充内容）。模板方法使得子类可以不改变一个算法的结构前提下，重新定义算法的某些特定步骤，模板方法模式把不变行为搬到超类中，从而去除了子类中的重复代码。
 
 优点：
    1.实现了代码复用
    2.能够灵活应对子步骤的变化，符合开放-封闭原则

缺点：
    因为引入了一个抽象类，如果具体实现过多的话，需要用户或开发人员需要花更多的时间去理清类之间的关系。
*/

namespace 模版方法模式
{
    class Program
    {
        static void Main(string[] args)
        {
            VagetableCookModel cook_qingcai = new 炒青菜();
            cook_qingcai.CookFlow();
            Console.ReadKey();
        }
    }

    public abstract class VagetableCookModel
    {
        public void CookFlow()
        {
            Console.WriteLine("开始烧菜");
            this.InputOil();
            this.HeatOil();
            this.InputVegetable();
            this.FryVetetable();
            Console.WriteLine("菜烧好了");
        }

        private void InputOil()
        {
            Console.WriteLine("将油倒入锅子中");
        }

        private void HeatOil()
        {
            Console.WriteLine("将油加热");
        }

        public abstract void InputVegetable();

        private void FryVetetable()
        {
            Console.WriteLine("翻炒");
        }
    }

    public class 炒青菜 : VagetableCookModel
    {
        public override void InputVegetable()
        {
            Console.WriteLine("将青菜放入锅子");
        }
    }

    public class 炒青椒 : VagetableCookModel
    {
        public override void InputVegetable()
        {
            Console.WriteLine("将青椒放入锅子");
        }
    }
}
