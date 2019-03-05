using BookShop.WebApp.Models;
using System.Web;
using System.Web.Mvc;

namespace BookShop.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //把原来的注释换成我们自己定义的
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new MyExceptionAttribute());
        }
    }
}