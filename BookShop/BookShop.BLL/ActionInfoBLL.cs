using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;
using BookShop.IBLL;

namespace BookShop.BLL
{
    public partial class ActionInfoBLL:BaseBLL<ActionInfo>,IActionInfoBLL
    {
        #region IUsersBLL 成员

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="userinfoParams"></param>
        /// <returns></returns>
        public IQueryable<ActionInfo> LoadSearchParams(Model.Params.ActionInfoParams userinfoParams)
        {
            var action = this.DbSession.ActionInfoDAL.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userinfoParams.ActionName))
            {
                action = action.Where<ActionInfo>(c => c.ActionName.Contains(userinfoParams.ActionName));
            }
            userinfoParams.TotalCount = action.Count();
            return action.OrderBy<ActionInfo, int>(c => c.ActionId).Skip<ActionInfo>((userinfoParams.PageIndex - 1) * userinfoParams.PageSize).Take<ActionInfo>(userinfoParams.PageSize);
        }

        #endregion

        #region 批量删除数据


        public bool DeleteEntities(List<int> list)
        {
            var actions = DbSession.ActionInfoDAL.LoadEntities(c => list.Contains(c.ActionId));
            foreach (ActionInfo actioninfo in actions)
            {
                this.DbSession.ActionInfoDAL.DeleteEntity(actioninfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}
