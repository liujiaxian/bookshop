using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;
using BookShop.IBLL;
using System.Web;
using System.IO;

namespace BookShop.BLL
{
    public partial class UsersBLL:BaseBLL<Users>,IUsersBLL
    {
        #region IUsersBLL 成员

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="userinfoParams"></param>
        /// <returns></returns>
        public IQueryable<Users> LoadSearchParams(Model.Params.UserInfoParams userinfoParams)
        {
            var user = this.DbSession.UsersDAL.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userinfoParams.UserName))
            {
                user = user.Where<Users>(c => c.LoginId.Contains(userinfoParams.UserName));
            }
            if (!string.IsNullOrEmpty(userinfoParams.UserEmail))
            {
                user = user.Where<Users>(c => c.Mail.Contains(userinfoParams.UserEmail));
            }
            //这里利用导航属性查找角色名
            if (userinfoParams.UserRole != "全部" && !string.IsNullOrEmpty(userinfoParams.UserRole))
            {
                user = user.Where<Users>(c => c.RoleInfo.RoleName.Contains(userinfoParams.UserRole));
            }
            if (userinfoParams.UserState != "全部" && !string.IsNullOrEmpty(userinfoParams.UserState))
            {
                user = user.Where<Users>(c => c.UserStates.Name.Contains(userinfoParams.UserState));
            }
            userinfoParams.TotalCount = user.Count();
            return user.OrderBy<Users, int>(c => c.Id).Skip<Users>((userinfoParams.PageIndex - 1) * userinfoParams.PageSize).Take<Users>(userinfoParams.PageSize);
        }

        #endregion

        #region 批量删除数据


        public bool DeleteEntities(List<int> list)
        {
            var users = DbSession.UsersDAL.LoadEntities(c => list.Contains(c.Id));
            foreach (Users userinfo in users)
            {
                this.DbSession.UsersDAL.DeleteEntity(userinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion

        #region 生成发送邮件的模板静态页内容发给用户邮箱
        /// <summary>
        /// 生成邮件内容页面
        /// </summary>
        /// <param name="id"></param>
        public string CreateHtmlPage(string email,string url)
        {
            Users user = this.DbSession.UsersDAL.LoadEntities(c => c.Mail==email).FirstOrDefault();
            string html = HttpContext.Current.Request.MapPath("/Templates/sendemail.html");
            html = File.ReadAllText(html);
            html = html.Replace("$Email", email).Replace("$Url", url).Replace("$Time", DateTime.Now.ToShortDateString());
            return html;
        }
        #endregion
    }
}
