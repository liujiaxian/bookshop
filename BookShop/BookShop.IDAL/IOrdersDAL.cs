using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.IDAL
{
    public partial interface IOrdersDAL:IBaseDAL<Orders>
    {
        decimal CreateOrder(string orderid,int userid,string address);
    }
}
