using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using BookShop.IDAL;
using System.Reflection;

namespace BookShop.DALFactory
{
    public partial class AbstractFactory
    {
        //
        //private static readonly string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
        //private static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
        //public static ICartDAL CreateCartDAL()
        //{
        //    string fullassemblyPath = NameSpace + ".CartDAL";
        //    return CreateInstance(fullassemblyPath,AssemblyPath) as ICartDAL;
        //}
        private static object CreateInstance(string fullassemblyPath,string assemblyPath)
        {
            //利用反射
            Assembly ass= Assembly.Load(assemblyPath);
            return ass.CreateInstance(fullassemblyPath);
        }
    }
}
