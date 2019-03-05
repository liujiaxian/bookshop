using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.WebApp.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        //
        // GET: /Admin/Base/
        //方便其它地方调用
        public Model.Users UserInfo { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            if (Session["admin"]==null)
            {
                Response.Redirect("/Admin/AdminLogin/Index");
            }
            else
            {
                UserInfo = Session["admin"] as Model.Users;
            }
        }
    }
}
