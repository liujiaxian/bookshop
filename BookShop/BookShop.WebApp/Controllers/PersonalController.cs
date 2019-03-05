using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Common;

namespace BookShop.WebApp.Controllers
{
    public class PersonalController : CheckLoginController
    {
        IBLL.IUsersBLL userbll = new BLL.UsersBLL();
        //
        // GET: /Personal/

        public ActionResult Index()
        {
            //查询用户信息
            if (Session["user"] != null)
            {
                Users user = Session["user"] as Users;
                var usermodel = userbll.LoadEntities(c => c.Id == user.Id).FirstOrDefault();
                if (usermodel != null)
                {
                    ViewData["usermodel"] = usermodel;
                }
                else
                {
                    Response.Redirect("/Account/Index");
                }
            }
            else
            {
                Response.Redirect("/Account/Index");
            }
            return View();
        }

        //更新数据
        public void UpdatePersonal(Users user)
        {
            string address_p = Request["location_p"];
            string address_c = Request["location_c"];
            string address_a = Request["location_a"];
            string address = address_p+address_c+ address_a+ Request["address"];
            string imgurl = Request["imgurl"];
            int id = Convert.ToInt32(Request["id"]);
            var model = userbll.LoadEntities(c => c.Id == id).FirstOrDefault();
            model.LoginId = user.LoginId;
            model.Name = user.Name;
            model.Phone = user.Phone;
            model.Address = address;
            model.Image = imgurl;
            if (string.IsNullOrEmpty(user.Name))
            {
                WebCommon.Show("姓名不能为空！");
            }
            else if (string.IsNullOrEmpty(user.Phone))
            {
                WebCommon.Show("电话不能为空！");
            }
            if (userbll.UpdateEntity(model))
            {
                WebCommon.ShowUrl("更新成功","/Personal/Index");
            }
            else
            {
                WebCommon.Show("更新失败");
            }           
        }
        //上传图片
        public void UploadImg()
        {
            foreach (string key in Request.Files)
            {
                //Response.Write(Request.Files[key].FileName);
                Request.Files[key].SaveAs(Server.MapPath("/Uploads/images/" + Request.Files[key].FileName));
                Response.Write("/Uploads/images/" + Request.Files[key].FileName);
            }
        }
    }
}
