using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.IBLL
{
    public partial interface IOrderBookBLL:IBaseBLL<OrderBook>
    {
        IQueryable<OrderBook> LoadSearchParams(Model.Params.OrderBookParams userinfoParams);
        bool DeleteEntities(List<int> list);

        bool DeleteEntitiesString(List<string> list);
    }
}
