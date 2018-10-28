using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service;
using ZSZ.DTO;

namespace ZSZ.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PermissionService myService = new PermissionService();
            var perms = myService.GetAll();
            foreach (var item in perms)
            {
                Console.WriteLine(item.Description);
            } 
            //using (ZSZDbContext ctx = new ZSZDbContext())
            //{
            //    //ctx.Database.Delete();
            //    //ctx.Database.Create();
            //    string s = ctx.Database.Connection.ConnectionString;
            //    Console.WriteLine("创建数据库" + s);
            //}
            //Assembly[] assemblies = new Assembly[] { Assembly.Load("ZSZ.Service") };
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
