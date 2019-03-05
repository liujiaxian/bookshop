using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Common;
using System.Drawing;
using BookShop.Model;

namespace BookShop.WebApp.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        IBLL.IUsersBLL userbll = new BLL.UsersBLL();
        IBLL.IT_EmailBLL emailbll = new BLL.T_EmailBLL();
        IBLL.ISettingsBLL settingsbll = new BLL.SettingsBLL();
        IBLL.IEmailActiveBLL emailactivebll = new BLL.EmailActiveBLL();
        //登录展示
        public ActionResult Index()
        {
            return View();
        }
        #region 登录功能
        public void Login()
        {
            string email = Request["Mail"];
            string pwd = Request["LoginPwd"];
            string vcode = Request["Vcode"];
            string redirecturl = HttpUtility.UrlDecode(Request["redirecturl"]);
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(pwd) && !string.IsNullOrEmpty(vcode))
            {
                if (Session["vcode"] != null && Session["vcode"].ToString() == vcode)
                {
                    string md5pwd = pwd.Md5();
                    var user = userbll.LoadEntities(c => c.Mail == email && c.LoginPwd == md5pwd).FirstOrDefault();
                    if (user != null)
                    {
                        Session["user"] = user;
                        Session.Remove("vcode");
                        if (!string.IsNullOrEmpty(redirecturl))
                        {
                            //WebCommon.GoNext("亲，登录成功了哦！", "正在返回登录前页面", HttpUtility.UrlDecode(redirect.ToString()));
                            WebCommon.Url(redirecturl);
                        }
                        else
                        {
                            WebCommon.Url("/Home/Index");
                        }                      
                    }
                    else
                    {
                        WebCommon.GoBack("亲，邮箱或密码不正确哦！");
                    }
                }
                else
                {
                    WebCommon.GoBack("亲，验证码不正确哦！");
                }
            }
            else
            {
                WebCommon.GoBack("亲，数据还没有填完哦！");
            }
        }
        #endregion
        //注册展示
        public ActionResult Register()
        {
            return View();
        }

        #region 注册逻辑
        public void Regist()
        {
            string email = Request["Mail"];
            string pwd = Request["LoginPwd"];
            string repwd = Request["LoginRepwd"];
            string vcode = Request["Vcode"];
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(pwd) && !string.IsNullOrEmpty(repwd) && !string.IsNullOrEmpty(vcode))
            {
                if (Session["vcode"] != null && Session["vcode"].ToString() == vcode)
                {
                    var user = userbll.LoadEntities(c => c.Mail == email).FirstOrDefault();
                    if (user == null)
                    {
                        Users usermodel = new Users();
                        usermodel.Mail = email;
                        usermodel.LoginId = email;
                        usermodel.LoginPwd = pwd.Md5();
                        usermodel.Name = string.Empty;
                        usermodel.Phone = string.Empty;
                        usermodel.Address = string.Empty;
                        usermodel.UserRoleId = 2;
                        usermodel.UserStateId = 1;
                        usermodel.Money = 0;
                        usermodel.Image = string.Empty;
                        usermodel.RegTime = DateTime.Now;
                        Users model = userbll.AddEntity(usermodel);
                        if (model != null)
                        {
                            WebCommon.GoNext("亲，恭喜注册成功！", "登录", "/Account/Index");
                        }
                        else
                        {
                            WebCommon.GoBack("亲，注册失败了哦！");
                        }
                    }
                    else
                    {
                        WebCommon.GoBack("亲，该邮箱已存在了哦！");
                    }
                }
                else
                {
                    WebCommon.GoBack("验证码不正确！");
                }
            }
            else
            {
                WebCommon.GoBack("数据填写不完整！");
            }
        }
        #endregion

        #region 忘记密码模块
        //展示页面
        public ActionResult ForgetPwd()
        {
            return View();
        }
        //逻辑代码
        [HttpPost]
        public void ForgetPwd(Users user)
        {
            //这里没有用 Request["Mail"]获取邮箱的值而是用MVC里的自动属性
            //用法就是：前台的表单name的值必须和数据库中的字段对应          
            if (!string.IsNullOrEmpty(user.Mail))
            {
                string vcode = Request["Vcode"];
                //校验验证码 这里判断session是否为空 一般用到session的地方 推荐判断下防止程序出错
                if (Session["vcode"] != null && Session["vcode"].ToString() == vcode)
                {
                    //查询是否存在该邮箱
                    var model = userbll.LoadEntities(c => c.Mail == user.Mail).FirstOrDefault();
                    if (model != null)
                    {
                        WebCommon.Url("/Account/LoginEmail?email=" + user.Mail);
                    }
                    else
                    {
                        WebCommon.GoBack("亲，系统不存在该邮箱哦！");
                    }
                }
                else
                {
                    WebCommon.GoBack("亲，验证码不正确哦！");
                }
            }
            else
            {
                WebCommon.GoBack("亲，邮箱不能为空哦！");
            }
        }
        //第二步展示登录邮箱界面
        public ActionResult LoginEmail()
        {
            string email = Request["email"];
            var model = userbll.LoadEntities(c => c.Mail == email).FirstOrDefault();
            if (model != null)
            {
                string[] str = email.Split('@');
                string emailstr = str[1];
                var emailmodel = emailbll.LoadEntities(c => c.Emaillast == emailstr).FirstOrDefault();
                if (emailmodel != null)
                {
                    //发送邮件
                    //调用邮件类之前先把邮件的参数从数据库中查出来
                    var smtp0 = settingsbll.LoadEntities(c => c.key == "系统邮件SMTP").FirstOrDefault();
                    var smtp1 = settingsbll.LoadEntities(c => c.key == "系统邮件端口号").FirstOrDefault();
                    var smtp2 = settingsbll.LoadEntities(c => c.key == "使用安全套接字层").FirstOrDefault();
                    var smtp3 = settingsbll.LoadEntities(c => c.key == "系统邮件用户名").FirstOrDefault();
                    var smtp4 = settingsbll.LoadEntities(c => c.key == "系统邮件标题").FirstOrDefault();
                    var smtp5 = settingsbll.LoadEntities(c => c.key == "系统邮件密码").FirstOrDefault();
                    if (smtp0 != null && smtp1 != null && smtp2 != null && smtp3 != null && smtp4 != null && smtp5 != null)
                    {
                        string[] smtp = new string[6];
                        smtp[0] = smtp0.value;
                        smtp[1] = smtp1.value;
                        smtp[2] = smtp2.value;
                        smtp[3] = smtp3.value;
                        smtp[4] = smtp4.value;
                        smtp[5] = smtp5.value;
                        //生成激活码
                        Guid guid = Guid.NewGuid();
                        string activetoken = guid.ToString().Md5();
                        //存到数据库中 判断是否存在该用胡
                        var isid = emailactivebll.LoadEntities(c => c.UserId == model.Id).FirstOrDefault();
                        if (isid != null)//说明用户使用过找回密码 无需添加新纪录 需要更新即可
                        {
                            isid.ActiveState = false;
                            isid.ActiveTime = DateTime.Now;
                            isid.ActiveToken = activetoken;
                            if (!emailactivebll.UpdateEntity(isid))
                            {
                                return Content("<script type='text/javascript'>alert('亲，发送邮件失败哦！');window.location.href='/Account/ForgetPwd'</script>");
                            }
                        }
                        else
                        {
                            EmailActive addmodel = new EmailActive();
                            addmodel.UserId = model.Id;
                            addmodel.ActiveToken = activetoken;
                            addmodel.ActiveState = false;
                            addmodel.ActiveTime = DateTime.Now;
                            var addemail = emailactivebll.AddEntity(addmodel);
                            if (addemail == null)
                            {
                                return Content("<script type='text/javascript'>alert('亲，发送邮件失败哦！');window.location.href='/Account/ForgetPwd'</script>");
                            }
                        }
                        //拼接激活的url
                        string host = Request.Url.Scheme + "://" + Request.Url.Authority;
                        string url = host + "/Account/ForgetRepwd?email=" + email + "&token=" + activetoken;
                        //获取内容
                        string htmlbody = userbll.CreateHtmlPage(email, url);
                        //调用邮件类发送邮件
                        bool send = MailHelper.Send(smtp, email, email + ",重设您在图书商城网的密码", htmlbody);
                        if (send)//发送成功
                        {
                            ViewData["emailurl"] = "http://" + emailmodel.Emailurl;
                            ViewData["emailname"] = email;
                            return View();
                        }
                        else
                        {
                            ViewData["emailurl"] = null;
                            return Content("<script type='text/javascript'>alert('亲，发送邮件失败哦！');window.location.href='/Account/ForgetPwd'</script>");
                        }
                    }
                    else
                    {
                        return Content("<script type='text/javascript'>alert('亲，发送邮件失败哦！');window.location.href='/Account/ForgetPwd'</script>");
                    }
                }
                else
                {
                    return Content("<script type='text/javascript'>alert('亲，发送邮件失败哦！');window.location.href='/Account/ForgetPwd'</script>");
                }
            }
            else
            {
                return Content("<script type='text/javascript'>alert('亲，非法操作哦！');window.location.href='/Account/ForgetPwd'</script>");
            }
        }
        //第三步重置密码视图展示
        public ActionResult ForgetRepwd()
        {
            //接收参数
            string email = Request["email"];
            string token = Request["token"];
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(token))
            {
                var usersel = userbll.LoadEntities(c => c.Mail == email).FirstOrDefault();
                if (usersel != null)
                {
                    var active = emailactivebll.LoadEntities(c => c.UserId == usersel.Id && c.ActiveToken == token).FirstOrDefault();
                    if (active != null)
                    {
                        if (active.ActiveTime.AddMinutes(30) > DateTime.Now)
                        {
                            if (active.ActiveState == false)
                            {
                                ViewData["forgetemail"] = email;
                                return View();
                            }
                            else
                            {
                                return Content("<script type='text/javascript'>alert('亲，该链接已经激活过了！');window.location.href='/Account/ForgetPwd'</script>");
                            }
                        }
                        else
                        {
                            return Content("<script type='text/javascript'>alert('亲，该链接已失效！');window.location.href='/Account/ForgetPwd'</script>");
                        }
                    }
                    else
                    {
                        return Content("<script type='text/javascript'>alert('亲，该链接已失效！');window.location.href='/Account/ForgetPwd'</script>");
                    }
                }
                else
                {
                    return Content("<script type='text/javascript'>alert('亲，该链接已失效！');window.location.href='/Account/ForgetPwd'</script>");
                }

            }
            else
            {
                return Content("<script type='text/javascript'>alert('亲，参数错误，请勿非法操作哦！');window.location.href='/Account/ForgetPwd'</script>");
            }

        }

        //重置密码逻辑
        [HttpPost]
        public void ForgetRepwd(Users user)
        {
            if (string.IsNullOrEmpty(user.LoginPwd) || string.IsNullOrEmpty(Request["LoginRepwd"]))
            {
                WebCommon.GoBack("亲，数据还没有填完哦！");
            }
            else
            {
                if (user.LoginPwd != Request["LoginRepwd"])
                {
                    WebCommon.GoBack("亲，密码与确认密码不一致哦！");
                }
                else
                {
                    var selid = userbll.LoadEntities(c => c.Mail == user.Mail).FirstOrDefault();
                    if (selid != null)
                    {
                        selid.LoginPwd = user.LoginPwd.Md5();
                        if (userbll.UpdateEntity(selid))
                        {
                            //将激活状态改为true
                            var activestate = emailactivebll.LoadEntities(c => c.UserId == selid.Id).FirstOrDefault();
                            activestate.ActiveState = true;
                            if (emailactivebll.UpdateEntity(activestate))
                            {
                                WebCommon.GoNext("重置密码成功", "登录页面", "/Account/Index");
                            }
                            else
                            {
                                WebCommon.GoBack("亲，激活状态更新失败了哦！");
                            }
                        }
                        else
                        {
                            WebCommon.GoBack("亲，更新失败了哦！");
                        }
                    }
                    else
                    {
                        WebCommon.ShowUrl("亲，参数错误，请重试！", "/Account/ForgetPwd");
                    }
                }
            }
        }
        #endregion

        #region 验证码
        //验证码显示
        public ActionResult Vcode()
        {
            var code = CaptchaHelper.CreateRandomCode(4);
            Session["vcode"] = code;
            var img = CaptchaHelper.DrawImage(code, 20, background: Color.White);
            return File(img, "Image/jpeg");
        }
        #endregion

        #region 注销
        public void Layout()
        {
            if (Session["user"] != null)
            {
                Session.Remove("user");
                WebCommon.GoNext("注销成功......", "登录", "/Account/Index");
            }
        }
        #endregion
    }
}
