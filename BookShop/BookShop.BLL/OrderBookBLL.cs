using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.BLL
{
    public partial class OrderBookBLL:BaseBLL<OrderBook>,IBLL.IOrderBookBLL
    {
        #region IUsersBLL 成员

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="userinfoParams"></param>
        /// <returns></returns>
        public IQueryable<OrderBook> LoadSearchParams(Model.Params.OrderBookParams userinfoParams)
        {
            var categories = this.DbSession.OrderBookDAL.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userinfoParams.OrderId))
            {
                categories = categories.Where<OrderBook>(c => c.OrderID.Contains(userinfoParams.OrderId));
            }
            userinfoParams.TotalCount = categories.Count();
            return categories.OrderBy<OrderBook, int>(c => c.Id).Skip<OrderBook>((userinfoParams.PageIndex - 1) * userinfoParams.PageSize).Take<OrderBook>(userinfoParams.PageSize);
        }

        #endregion

        #region 批量删除数据


        public bool DeleteEntities(List<int> list)
        {
            var categories = DbSession.OrderBookDAL.LoadEntities(c => list.Contains(c.Id));
            foreach (OrderBook categoriesinfo in categories)
            {
                this.DbSession.OrderBookDAL.DeleteEntity(categoriesinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }

        public bool DeleteEntitiesString(List<string> list)
        {
            var categories = DbSession.OrderBookDAL.LoadEntities(c => list.Contains(c.OrderID));
            foreach (OrderBook categoriesinfo in categories)
            {
                this.DbSession.OrderBookDAL.DeleteEntity(categoriesinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}
