using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;

namespace BookShop.IBLL
{
    public partial interface IBooksBLL
    {
        int GetPageCount(int pageSize);
        IQueryable<Books> LoadSearchParams(Model.Params.BooksParams userinfoParams);
        bool DeleteEntities(List<int> list);
    }
}
