using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;
using BookShop.IBLL;

namespace BookShop.BLL
{
    public partial class CategoriesBLL:BaseBLL<Categories>,ICategoriesBLL
    {
        #region IUsersBLL 成员

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="userinfoParams"></param>
        /// <returns></returns>
        public IQueryable<Categories> LoadSearchParams(Model.Params.CategoriesParams userinfoParams)
        {
            var categories = this.DbSession.CategoriesDAL.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userinfoParams.CategoriesName))
            {
                categories = categories.Where<Categories>(c => c.Name.Contains(userinfoParams.CategoriesName));
            }
            userinfoParams.TotalCount = categories.Count();
            return categories.OrderBy<Categories, int>(c => c.Id).Skip<Categories>((userinfoParams.PageIndex - 1) * userinfoParams.PageSize).Take<Categories>(userinfoParams.PageSize);
        }

        #endregion

        #region 批量删除数据


        public bool DeleteEntities(List<int> list)
        {
            var categories = DbSession.CategoriesDAL.LoadEntities(c => list.Contains(c.Id));
            foreach (Categories categoriesinfo in categories)
            {
                this.DbSession.CategoriesDAL.DeleteEntity(categoriesinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}
