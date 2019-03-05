using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace BookShop.DAL
{
    public partial class OrdersDAL:BaseDAL<Orders>,IDAL.IOrdersDAL
    {
        #region IOrdersDAL 成员

        public decimal CreateOrder(string orderid, int userid, string address)
        {
            bookshopEntities db = (bookshopEntities)DbContextFactory.CreateDbContext();
            decimal totalMoney = 0;
            var par = new ObjectParameter("TotalPrice", totalMoney);
            db.CreateOrder(orderid,userid,address,par);
            return Convert.ToDecimal(par.Value);
        }
        #endregion
    }
}
