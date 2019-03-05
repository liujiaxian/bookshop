using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;
using BookShop.IBLL;

namespace BookShop.BLL
{
    public partial class SettingsBLL:BaseBLL<Settings>,ISettingsBLL
    {
        #region IUsersBLL 成员

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="userinfoParams"></param>
        /// <returns></returns>
        public IQueryable<Settings> LoadSearchParams(Model.Params.SettingsParams userinfoParams)
        {
            var settings = this.DbSession.SettingsDAL.LoadEntities(c => true);
            if (!string.IsNullOrEmpty(userinfoParams.Key))
            {
                settings = settings.Where<Settings>(c => c.key.Contains(userinfoParams.Key));
            }
            if (!string.IsNullOrEmpty(userinfoParams.Value))
            {
                settings = settings.Where<Settings>(c => c.value.Contains(userinfoParams.Value));
            }
            userinfoParams.TotalCount = settings.Count();
            return settings.OrderBy<Settings, int>(c => c.id).Skip<Settings>((userinfoParams.PageIndex - 1) * userinfoParams.PageSize).Take<Settings>(userinfoParams.PageSize);
        }

        #endregion

        #region 批量删除数据


        public bool DeleteEntities(List<int> list)
        {
            var settings = DbSession.SettingsDAL.LoadEntities(c => list.Contains(c.id));
            foreach (Settings settingsinfo in settings)
            {
                this.DbSession.SettingsDAL.DeleteEntity(settingsinfo);
            }
            return this.DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}
