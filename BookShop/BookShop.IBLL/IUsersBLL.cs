using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;

namespace BookShop.IBLL
{
    public partial interface IUsersBLL
    {
        IQueryable<Users> LoadSearchParams(Model.Params.UserInfoParams userinfoParams);
        bool DeleteEntities(List<int> list);

        string CreateHtmlPage(string email, string url);
    }
}
