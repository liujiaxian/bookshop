using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.BLL
{
    public partial class OrdersBLL : BaseBLL<Orders>, IBLL.IOrdersBLL
    {
        #region 批量删除数据


        public bool DeleteEntities(List<string> list)
        {
            var users = DbSession.OrdersDAL.LoadEntities(c => list.Contains(c.OrderId));
            foreach (Orders userinfo in users)
            {
                this.DbSession.OrdersDAL.DeleteEntity(userinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion

        #region IOrdersBLL 成员


        public decimal CreateOrder(string orderid, int userid, string address)
        {
            return this.DbSession.OrdersDAL.CreateOrder(orderid, userid, address);
        }

        #endregion
    }
}
