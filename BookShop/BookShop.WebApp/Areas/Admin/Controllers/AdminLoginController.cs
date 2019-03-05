using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Common;

namespace BookShop.WebApp.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        //
        // GET: /Admin/AdminLogin/
        IBLL.IUsersBLL userbll = new BLL.UsersBLL();
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                Response.Redirect("/Admin/AdminHome/Index");
            }
            else if (Request.Cookies["name"] != null && Request.Cookies["pwd"] != null)
            {
                ViewData["name"] = Request.Cookies["name"].Value;
                ViewData["pwd"] = Request.Cookies["pwd"].Value;
            }
            return View();
        }
        public void Login()
        {
            bool IsPostBack = Convert.ToBoolean(Request["post"]);
            if (IsPostBack)
            {
                string name = Request["username"];
                string pwd = Request["password"];
                bool checkme = Convert.ToBoolean(Request["remember-me"]);
                if (string.IsNullOrEmpty(name))
                {
                    WebCommon.GoBack("账号不能为空！");
                }
                else if (string.IsNullOrEmpty(pwd))
                {
                    WebCommon.GoBack("密码不能为空！");
                }
                else
                {
                    var status = userbll.LoadEntities(c => c.LoginId == name && c.UserStateId == 1).FirstOrDefault();
                    if (status != null)
                    {
                        string md5pwd = pwd.Md5();
                        var user = userbll.LoadEntities(c => c.LoginId == name && c.LoginPwd == md5pwd).FirstOrDefault();
                        if (user != null)
                        {
                            var role = userbll.LoadEntities(c => c.LoginId == name && c.LoginPwd == md5pwd && c.UserRoleId == 1).FirstOrDefault();
                            if (role != null)
                            {
                                if (checkme)//记住我
                                {
                                    HttpCookie cp1 = new HttpCookie("name", name);
                                    HttpCookie cp2 = new HttpCookie("pwd", pwd);
                                    cp1.Expires = DateTime.Now.AddDays(7);
                                    cp2.Expires = DateTime.Now.AddDays(7);
                                    Response.Cookies.Add(cp1);
                                    Response.Cookies.Add(cp2);
                                }
                                Session["admin"] = user;
                                WebCommon.GoNext("登录成功", "首页", "/Admin/AdminHome/Index");
                            }
                            else
                            {
                                WebCommon.GoBack("对不起，你不是管理员！");
                            }
                        }
                        else
                        {
                            WebCommon.GoBack("用户名或密码错误！");
                        }
                    }
                    else
                    {
                        WebCommon.GoBack("对不起，你的账号已被锁定！");
                    }
                }
            }
            else
            {
                Response.Redirect("/Admin/AdminLogin/Index");
            }
        }
    }
}
