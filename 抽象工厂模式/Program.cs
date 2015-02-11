using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    抽象工厂应用场景：多种数据库支持、多语言、操作系统控件等。
*/

namespace 抽象工厂模式
{
    class Program
    {
        static void Main(string[] args)
        {
            简单套餐 taocan1 = new 套餐二();
            Food food1 = taocan1.SomethingToEat();
            Drink drink1 = taocan1.SomethingToDrink();
            double totalPrice = food1.Price() + drink1.Price();

            Console.WriteLine(totalPrice.ToString());
            Console.ReadKey();
        }

        public abstract class Food
        {
            public abstract double Price();
        }

        public class 汉堡 : Food
        {
            public override double Price()
            {
                return 10.8;
            }
        }

        public class 薯条 : Food
        {
            public override double Price()
            {
                return 3.6;
            }
        }

        public abstract class Drink
        {
            public abstract double Price();
        }

        public class 可乐 : Drink
        {
            public override double Price()
            {
                return 5;
            }
        }

        public class 美年达 : Drink
        {
            public override double Price()
            {
                return 4.8;
            }
        }

        /// <summary>
        /// 抽象工厂
        /// </summary>
        public abstract class 简单套餐
        {
            public abstract Food SomethingToEat();
            public abstract Drink SomethingToDrink();
        }

        public abstract class 多人套餐
        {
            public abstract List<Food> LotsOfThingsToEat();
            public abstract List<Drink> LotsOfThingsToDrink();
        }

        public class 套餐一 : 简单套餐
        {
            public override Food SomethingToEat()
            {
                return new 汉堡();
            }

            public override Drink SomethingToDrink()
            {
                return new 可乐();
            }
        }

        public class 套餐二 : 简单套餐
        {
            public override Food SomethingToEat()
            {
                return new 薯条();
            }

            public override Drink SomethingToDrink()
            {
                return new 美年达();
            }
        }

        public class 全家桶 : 多人套餐
        {
            public override List<Drink> LotsOfThingsToDrink()
            {
                return new List<Drink>(){
                    new 可乐(),
                    new 美年达()
                };
            }

            public override List<Food> LotsOfThingsToEat()
            {
                return new List<Food>(){
                    new 汉堡(),
                    new 薯条()
                };
            }
        }
    }
}
