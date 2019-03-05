using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Model.Params
{
    public class CommentParams:BaseParams
    {
        public string Mail { get; set; }
        public string Title { get; set; }
    }
}
