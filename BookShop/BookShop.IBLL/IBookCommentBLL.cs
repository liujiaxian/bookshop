using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.IBLL
{
    public partial interface IBookCommentBLL : IBaseBLL<BookComment>
    {
        IQueryable<BookComment> LoadSearchParams(Model.Params.CommentParams userinfoParams);
        bool DeleteEntities(List<int> list);
        int GetPageCount(int pageSize,int id);
    }
}
