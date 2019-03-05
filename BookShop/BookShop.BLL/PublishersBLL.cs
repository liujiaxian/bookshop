using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;

namespace BookShop.BLL
{
    public partial class PublishersBLL:BaseBLL<Publishers>,IBLL.IPublishersBLL
    {
        #region IUsersBLL 成员

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="userinfoParams"></param>
        /// <returns></returns>
        public IQueryable<Publishers> LoadSearchParams(Model.Params.PublisherParams userinfoParams)
        {
            var categories = this.DbSession.PublishersDAL.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userinfoParams.PublisherName))
            {
                categories = categories.Where<Publishers>(c => c.Name.Contains(userinfoParams.PublisherName));
            }
            userinfoParams.TotalCount = categories.Count();
            return categories.OrderBy<Publishers, int>(c => c.Id).Skip<Publishers>((userinfoParams.PageIndex - 1) * userinfoParams.PageSize).Take<Publishers>(userinfoParams.PageSize);
        }

        #endregion

        #region 批量删除数据


        public bool DeleteEntities(List<int> list)
        {
            var categories = DbSession.PublishersDAL.LoadEntities(c => list.Contains(c.Id));
            foreach (Publishers categoriesinfo in categories)
            {
                this.DbSession.PublishersDAL.DeleteEntity(categoriesinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}
