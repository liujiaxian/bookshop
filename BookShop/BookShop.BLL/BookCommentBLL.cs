using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.BLL
{
    public partial class BookCommentBLL:BaseBLL<BookComment>,IBLL.IBookCommentBLL
    {
        #region IUsersBLL 成员

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="userinfoParams"></param>
        /// <returns></returns>
        public IQueryable<BookComment> LoadSearchParams(Model.Params.CommentParams userinfoParams)
        {
            var user = this.DbSession.BookCommentDAL.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userinfoParams.Mail))
            {
                user = user.Where<BookComment>(c => c.Users.Mail.Contains(userinfoParams.Mail));
            }
            if (!string.IsNullOrEmpty(userinfoParams.Title))
            {
                user = user.Where<BookComment>(c => c.Books.Title.Contains(userinfoParams.Title));
            }
            userinfoParams.TotalCount = user.Count();
            return user.OrderBy<BookComment, int>(c => c.Id).Skip<BookComment>((userinfoParams.PageIndex - 1) * userinfoParams.PageSize).Take<BookComment>(userinfoParams.PageSize);
        }

        #endregion

        #region 批量删除数据


        public bool DeleteEntities(List<int> list)
        {
            var users = DbSession.BookCommentDAL.LoadEntities(c => list.Contains(c.Id));
            foreach (BookComment userinfo in users)
            {
                this.DbSession.BookCommentDAL.DeleteEntity(userinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion

        public int GetPageCount(int pageSize,int id)
        {
            int count = DbSession.BookCommentDAL.LoadEntities(c => c.BookId==id).Count();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
            return pageCount;
        }
    }
}
