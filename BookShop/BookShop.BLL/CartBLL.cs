using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;
using BookShop.IBLL;

namespace BookShop.BLL
{
    public partial class CartBLL:BaseBLL<Cart>,ICartBLL
    {
        //确定当前的DAL
        //public override void SetCurrentDAL()
        //{
        //    CurrentDAL = this.DbSession.CartDAL;
        //}

        #region IUsersBLL 成员

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="userinfoParams"></param>
        /// <returns></returns>
        public IQueryable<Cart> LoadSearchParams(Model.Params.CartParams userinfoParams)
        {
            var user = this.DbSession.CartDAL.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userinfoParams.Mail))
            {
                user = user.Where<Cart>(c => c.Users.Mail.Contains(userinfoParams.Mail));
            }
            if (!string.IsNullOrEmpty(userinfoParams.Title))
            {
                user = user.Where<Cart>(c => c.Books.Title.Contains(userinfoParams.Title));
            }         
            userinfoParams.TotalCount = user.Count();
            return user.OrderBy<Cart, int>(c => c.Id).Skip<Cart>((userinfoParams.PageIndex - 1) * userinfoParams.PageSize).Take<Cart>(userinfoParams.PageSize);
        }

        #endregion

        #region 批量删除数据


        public bool DeleteEntities(List<int> list)
        {
            var users = DbSession.CartDAL.LoadEntities(c => list.Contains(c.Id));
            foreach (Cart userinfo in users)
            {
                this.DbSession.CartDAL.DeleteEntity(userinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}
