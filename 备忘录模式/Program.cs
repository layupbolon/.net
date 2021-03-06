﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 定义：在不破坏封装的前提下，捕获一个对象的内部状态，并在该对象之外保存这个状态，这样以后就可以把该对象恢复到原先的状态。
 优点：
    •如果某个操作错误地破坏了数据的完整性，此时可以使用备忘录模式将数据恢复成原来正确的数据。
    •备份的状态数据保存在发起人角色之外，这样发起人就不需要对各个备份的状态进行管理。而是由备忘录角色进行管理，而备忘录角色又是由管理者角色管理，符合单一职责原则。
 缺点：
    •在实际的系统中，可能需要维护多个备份，需要额外的资源，这样对资源的消耗比较严重。
 适用场景：
    •如果系统需要提供回滚操作时，使用备忘录模式非常合适。例如文本编辑器的Ctrl+Z撤销操作的实现，数据库中事务操作。
 */

namespace 备忘录模式
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ContactPerson> contactPersons = new List<ContactPerson>()
            {
                new ContactPerson(){Name="张三",MobileNumber="123456789"},
                new ContactPerson(){Name="李四",MobileNumber="987498416"},
                new ContactPerson(){Name="王五",MobileNumber="284589891"}
            };

            MobileOwner owner = new MobileOwner(contactPersons);
            Administrator admin = new Administrator();
            admin.MemetoDictionary.TryAdd(DateTime.Now.ToString(), owner.CreateMemento());
            owner.Show();

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("删掉王五联系人");
            Console.WriteLine("--------------------------------------");
            owner.ContactPersons.RemoveAt(2);
            admin.MemetoDictionary.TryAdd(DateTime.Now.ToString(), owner.CreateMemento());
            owner.Show();

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("还原之前备份的联系人列表");

            Memento memento = new Memento(null);
            if (admin.MemetoDictionary.TryGetValue(admin.MemetoDictionary.Keys.First(), out memento))
            {
                owner.RestoreMemeto(memento);

                Console.WriteLine("--------------------------------------");
                owner.Show();
            }
            else
                Console.WriteLine("获取备份失败");

            Console.ReadKey();
        }
    }

    public class ContactPerson
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
    }

    public class MobileOwner
    {
        public List<ContactPerson> ContactPersons { get; set; }

        public MobileOwner(List<ContactPerson> _contactPersons)
        {
            this.ContactPersons = _contactPersons;
        }

        public Memento CreateMemento()
        {
            return new Memento(new List<ContactPerson>(this.ContactPersons));
        }

        public void RestoreMemeto(Memento memeto)
        {
            if (memeto != null)
            {
                this.ContactPersons = memeto.ContactPersons;
            }
        }

        public void Show()
        {
            Console.WriteLine("当前联系人列表中有{0}个人，他们是:", ContactPersons.Count);
            foreach (ContactPerson p in ContactPersons)
            {
                Console.WriteLine("姓名: {0} 号码为: {1}", p.Name, p.MobileNumber);
            }
        }
    }

    public class Memento
    {
        public List<ContactPerson> ContactPersons { get; set; }
        public Memento(List<ContactPerson> _contactPersons)
        {
            this.ContactPersons = _contactPersons;
        }
    }

    public class Administrator
    {
        //线程安全字典
        public System.Collections.Concurrent.ConcurrentDictionary<string, Memento> MemetoDictionary { get; set; }
        public Administrator()
        {
            MemetoDictionary = new System.Collections.Concurrent.ConcurrentDictionary<string, Memento>();
        }
    }
}
