using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.WebApp.Models
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        //将信息写到队列中
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            //捕获异常信息
            //ExceptionQueue.Enqueue(filterContext.Exception);
            //跳转到错误页面
            //filterContext.HttpContext.Response.Redirect("/Home/ShowMsg?Txt=程序出错了哦&Msg=首页&Url=/Home/Index");
            base.OnException(filterContext);
        }
    }
}