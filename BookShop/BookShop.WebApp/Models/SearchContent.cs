using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.WebApp.Models
{
    public class SearchContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string Msg { get; set; }
    }
}