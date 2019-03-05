using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.IBLL
{
    public partial interface IOrdersBLL : IBaseBLL<Orders>
    {
        bool DeleteEntities(List<string> list);

        decimal CreateOrder(string orderid, int userid, string address);
    }
}
