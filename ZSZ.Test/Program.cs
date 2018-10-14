using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service;

namespace ZSZ.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (ZSZDbContext ctx = new ZSZDbContext())
            //{
            //    //ctx.Database.Delete();
            //    //ctx.Database.Create();
            //    Console.WriteLine("创建数据库");
            //}
            Assembly[] assemblies = new Assembly[] { Assembly.Load("ZSZ.Service") };
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
