using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.IBLL
{
    public partial interface ISettingsBLL
    {
        IQueryable<Settings> LoadSearchParams(Model.Params.SettingsParams userinfoParams);
        bool DeleteEntities(List<int> list);
    }
}
