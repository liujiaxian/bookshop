using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;
using BookShop.IBLL;

namespace BookShop.BLL
{
    public partial class BooksBLL:BaseBLL<Books>,IBooksBLL
    {
        #region IBooksBLL 成员
        public int GetPageCount(int pageSize)
        {
            int count = DbSession.BooksDAL.LoadEntities(c => c.CategoryId==81).Count();
            int pageCount = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
            return pageCount;
        }
        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="userinfoParams"></param>
        /// <returns></returns>
        public IQueryable<Books> LoadSearchParams(Model.Params.BooksParams userinfoParams)
        {
            var book = this.DbSession.BooksDAL.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userinfoParams.Title))
            {
                book = book.Where<Books>(c => c.Title.Contains(userinfoParams.Title));
            }
            if (!string.IsNullOrEmpty(userinfoParams.Author))
            {
                book = book.Where<Books>(c => c.Author.Contains(userinfoParams.Author));
            }
            if (userinfoParams.Publisher != "全部" && !string.IsNullOrEmpty(userinfoParams.Publisher))
            {
                book = book.Where<Books>(c => c.Publishers.Name.Contains(userinfoParams.Publisher));
            }
            if (userinfoParams.Categories != "全部" && !string.IsNullOrEmpty(userinfoParams.Categories))
            {
                book = book.Where<Books>(c => c.Categories.Name.Contains(userinfoParams.Categories));
            }
            userinfoParams.TotalCount = book.Count();
            return book.OrderBy<Books, int>(c => c.Id).Skip<Books>((userinfoParams.PageIndex - 1) * userinfoParams.PageSize).Take<Books>(userinfoParams.PageSize);
        }

        #endregion

        #region 批量删除数据


        public bool DeleteEntities(List<int> list)
        {
            var Books = DbSession.BooksDAL.LoadEntities(c => list.Contains(c.Id));
            foreach (Books bookinfo in Books)
            {
                this.DbSession.BooksDAL.DeleteEntity(bookinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}
