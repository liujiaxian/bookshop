using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.IBLL
{
    public partial interface IActionInfoBLL
    {
        IQueryable<ActionInfo> LoadSearchParams(Model.Params.ActionInfoParams userinfoParams);
        bool DeleteEntities(List<int> list);
    }
}
