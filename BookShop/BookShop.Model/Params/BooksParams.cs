using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Model.Params
{
    public class BooksParams:BaseParams
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Categories { get; set; }
    }
}
