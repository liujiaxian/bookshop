using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.IBLL
{
    public partial interface ICategoriesBLL
    {
        IQueryable<Categories> LoadSearchParams(Model.Params.CategoriesParams userinfoParams);
        bool DeleteEntities(List<int> list);
    }
}
