using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Model.Params
{
    public class SettingsParams:BaseParams
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
