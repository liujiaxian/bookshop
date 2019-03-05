using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;

namespace BookShop.IBLL
{
    public partial interface IPublishersBLL:IBaseBLL<Publishers>
    {
        IQueryable<Publishers> LoadSearchParams(Model.Params.PublisherParams userinfoParams);
        bool DeleteEntities(List<int> list);
    }
}
