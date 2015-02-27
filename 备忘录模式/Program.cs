using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 备忘录模式
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<ContactPerson> contactPersons = new List<ContactPerson>()
            //{
            //    new ContactPerson(){Name="张三",MobileNumber="123456789"},
            //    new ContactPerson(){Name="李四",MobileNumber="987498416"},
            //    new ContactPerson(){Name="王五",MobileNumber="284589891"}
            //};

            //MobileOwner owner = new MobileOwner(contactPersons);
            //Administrator admin = new Administrator();
            //admin.MemetoDictionary.TryAdd(DateTime.Now.ToString(), owner.CreateMemento());
            //owner.Show();

            //Console.WriteLine("--------------------------------------");
            //Console.WriteLine("删掉王五联系人");
            //Console.WriteLine("--------------------------------------");
            //owner.ContactPersons.RemoveAt(2);
            //admin.MemetoDictionary.TryAdd(DateTime.Now.ToString(), owner.CreateMemento());
            //owner.Show();

            //Console.WriteLine("--------------------------------------");
            //Console.WriteLine("还原之前备份的联系人列表");

            //Memento memento = new Memento(null);
            //if (admin.MemetoDictionary.TryGetValue(admin.MemetoDictionary.Keys.First(), out memento))
            //{
            //    owner.RestoreMemeto(memento);

            //    Console.WriteLine("--------------------------------------");
            //    owner.Show();
            //}
            //else
            //    Console.WriteLine("获取备份失败");


            System.Collections.Concurrent.ConcurrentDictionary<int, int> dictionary = new System.Collections.Concurrent.ConcurrentDictionary<int, int>();

            var actions = new List<Action>();
            for (int i = 0; i < 10000; i++)
            {
                actions.Add(() => { dictionary.TryAdd(i, i); });
            }

            var result = (from a in actions.AsParallel().WithDegreeOfParallelism(5)
                select a).ToArray();

            Parallel.Invoke(result);

            if (dictionary.Count>0)
            {
                foreach (KeyValuePair<int, int> keyValuePair in dictionary)
                {
                    Console.WriteLine("key:{0} value:{1}", keyValuePair.Key, keyValuePair.Value);
                }
            }
            else
                Console.WriteLine("字典为空");

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
