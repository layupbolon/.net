﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 定义：将一个复杂对象的构建与它的表示分离，使得同样的构建过程可以创建不同的表示。
 建造模式的实现要点：
    1.在建造者模式中，指挥者是直接与客户端打交道的，指挥者将客户端创建产品的请求划分为对各个部件的建造请求，再将这些请求委派到具体建造者角色，具体建造者角色是完成具体产品的构建工作的，却不为客户所知道。
    2.建造者模式主要用于“分步骤来构建一个复杂的对象”，其中“分步骤”是一个固定的组合过程，而复杂对象的各个部分是经常变化的（也就是说电脑的内部组件是经常变化的，这里指的的变化如硬盘的大小变了，CPU由单核变双核等）。
    3.产品不需要抽象类，由于建造模式的创建出来的最终产品可能差异很大，所以不大可能提炼出一个抽象产品类。
    4.在前面文章中介绍的抽象工厂模式解决了“系列产品”的需求变化，而建造者模式解决的是 “产品部分” 的需要变化。
    5.由于建造者隐藏了具体产品的组装过程，所以要改变一个产品的内部表示，只需要再实现一个具体的建造者就可以了，从而能很好地应对产品组成组件的需求变化。
 */

namespace 创造者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Order boss = new Order();

            Builder xiaoming = new Builder_XiaoMing();
            boss.OrdBuilderToWork(xiaoming);
            Computer computer_1 = xiaoming.GetComputer();
            computer_1.Show();

            Console.WriteLine("------------------------------");

            Builder zhangsan = new Builder_ZhangSan();
            boss.OrdBuilderToWork(zhangsan);
            Computer computer_2 = zhangsan.GetComputer();
            computer_2.Show();

            Console.ReadKey();
        }
    }

    public abstract class Builder
    {
        public abstract void InstallCPU();
        public abstract void InstallMainBoard();
        public abstract Computer GetComputer();
    }

    public class Computer
    {
        public Computer(string computerName)
        {
            this.ComputerName = computerName;
        }

        public string ComputerName { get; private set; }
        private List<string> components = new List<string>();

        public void Install(string component)
        {
            if (!components.Contains(component))
                components.Add(component);
        }

        public void Unstall(string component)
        {
            if (components.Contains(component))
                components.Remove(component);
        }

        public void Show()
        {
            if (components != null && components.Any())
            {
                components.ForEach(item =>
                {
                    Console.WriteLine("{0}已安装了{1}", ComputerName, item);
                });
            }
        }
    }

    public class Order
    {
        public void OrdBuilderToWork(Builder builder)
        {
            builder.InstallCPU();
            builder.InstallMainBoard();
        }
    }

    public class Builder_XiaoMing : Builder
    {
        private Computer computer = new Computer("小明组装的电脑");

        public override void InstallCPU()
        {
            computer.Install("CPU1");
        }

        public override void InstallMainBoard()
        {
            computer.Install("Main Board1");
        }

        public override Computer GetComputer()
        {
            return this.computer;
        }
    }

    public class Builder_ZhangSan : Builder
    {
        private Computer computer = new Computer("张三组装的电脑");

        public override void InstallCPU()
        {
            computer.Install("CPU2");
        }

        public override void InstallMainBoard()
        {
            computer.Install("Main Board2");
        }

        public override Computer GetComputer()
        {
            return this.computer;
        }
    }
}
