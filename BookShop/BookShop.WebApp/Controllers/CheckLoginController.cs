using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Common;
using BookShop.Model;

namespace BookShop.WebApp.Controllers
{
    public class CheckLoginController : Controller
    {
        public Users UserInfo { get; set; }
        // GET: /CheckLogin/
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["user"] == null)
            {
                if (Request.RequestType != "POST")
                {
                    //WebCommon.GoNext("亲，还没有登录哦！", "登录", "/Account/Index");
                    Response.Redirect("/Account/Index?redirecturl="+HttpUtility.UrlEncode(Request.Url.ToString()));
                }
            }
            else
            {
                UserInfo = Session["user"] as Users;
            }
        }
    }
}
