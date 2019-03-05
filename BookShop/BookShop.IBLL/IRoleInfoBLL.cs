using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.IBLL
{
    public partial interface IRoleInfoBLL
    {
        IQueryable<RoleInfo> LoadSearchParams(Model.Params.RoleInfoParams userinfoParams);
        bool DeleteEntities(List<int> list);
    }
}
