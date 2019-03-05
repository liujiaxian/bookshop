using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Common
{
    public class MyPageBar
    {
        public static string PageSort(int pageIndex, int pageCount, int id)
        {
            if (pageCount == 1)
            {
                return string.Empty;
            }
            int start = pageIndex - 5;
            start = start < 1 ? 1 : start;
            int end = start + 9;
            end = end > pageCount ? pageCount : end;
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (pageIndex == i)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href='?id=" + id + "&page={0}'>{0}</a>", i));
                }
            }
            return sb.ToString();
        }
        public static string PageSortSearch(int pageIndex, int pageCount, string searchname)
        {
            if (pageCount == 1)
            {
                return string.Empty;
            }
            int start = pageIndex - 5;
            start = start < 1 ? 1 : start;
            int end = start + 9;
            end = end > pageCount ? pageCount : end;
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (pageIndex == i)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href='?searchText=" + searchname + "&page={0}'>{0}</a>", i));
                }
            }
            return sb.ToString();
        }
        //购物车分页
        public static string PageSortCart(int pageIndex, int pageCount)
        {
            if (pageCount == 1)
            {
                return string.Empty;
            }
            int start = pageIndex - 5;
            start = start < 1 ? 1 : start;
            int end = start + 9;
            end = end > pageCount ? pageCount : end;
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (pageIndex == i)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href='?page={0}'>{0}</a>", i));
                }
            }
            return sb.ToString();
        }

        //订单分页
        public static string PageSortOrder(int pageIndex, int pageCount)
        {
            if (pageCount == 1)
            {
                return string.Empty;
            }
            int start = pageIndex - 5;
            start = start < 1 ? 1 : start;
            int end = start + 9;
            end = end > pageCount ? pageCount : end;
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (pageIndex == i)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href='?action=cart&page={0}'>{0}</a>", i));
                }
            }
            return sb.ToString();
        }

        //订单详细分页
        public static string PageSortOrderDetail(int pageIndex, int pageCount,string orderid)
        {
            if (pageCount == 1)
            {
                return string.Empty;
            }
            int start = pageIndex - 5;
            start = start < 1 ? 1 : start;
            int end = start + 9;
            end = end > pageCount ? pageCount : end;
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (pageIndex == i)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href='?orderid={1}&page={0}'>{0}</a>", i,orderid));
                }
            }
            return sb.ToString();
        }
    }
}
