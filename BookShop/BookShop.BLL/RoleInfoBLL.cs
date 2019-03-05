using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;
using BookShop.IBLL;

namespace BookShop.BLL
{
    public partial class RoleInfoBLL:BaseBLL<RoleInfo>,IRoleInfoBLL
    {
        #region IUsersBLL 成员

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="userinfoParams"></param>
        /// <returns></returns>
        public IQueryable<RoleInfo> LoadSearchParams(Model.Params.RoleInfoParams userinfoParams)
        {
            var role = this.DbSession.RoleInfoDAL.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userinfoParams.UserRole))
            {
                role = role.Where<RoleInfo>(c => c.RoleName.Contains(userinfoParams.UserRole));
            }
            userinfoParams.TotalCount = role.Count();
            return role.OrderBy<RoleInfo, int>(c => c.RoleId).Skip<RoleInfo>((userinfoParams.PageIndex - 1) * userinfoParams.PageSize).Take<RoleInfo>(userinfoParams.PageSize);
        }

        #endregion

        #region 批量删除数据


        public bool DeleteEntities(List<int> list)
        {
            var roles = DbSession.RoleInfoDAL.LoadEntities(c => list.Contains(c.RoleId));
            foreach (RoleInfo roleinfo in roles)
            {
                this.DbSession.RoleInfoDAL.DeleteEntity(roleinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}
