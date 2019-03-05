using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;

namespace BookShop.IBLL
{
    public partial interface ICartBLL:IBaseBLL<Cart>
    {
        IQueryable<Cart> LoadSearchParams(Model.Params.CartParams userinfoParams);
        bool DeleteEntities(List<int> list);
    }
}
