using BookShop.Common;
using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.BLL;
using BookShop.IBLL;

namespace BookShop.WebApp.Controllers
{
    public class ShopController : CheckLoginController
    {
        IBLL.IBooksBLL bookbll = new BLL.BooksBLL();
        IBLL.ICartBLL cartbll = new BLL.CartBLL();
        IBLL.IOrderBookBLL orderbookbll = new BLL.OrderBookBLL();
        IBLL.IOrdersBLL orderbll = new BLL.OrdersBLL();
        IBLL.IUsersBLL userbll = new BLL.UsersBLL();
        Irecommend_ratingBLL ratingbll = new recommend_ratingBLL();
        Irecommend_logBLL logbll = new recommend_logBLL();
        IArticelWordsBLL abll = new ArticelWordsBLL();
        IBookCommentBLL commentbll = new BookCommentBLL();
        //
        // GET: /Shop/
        //购物车页面
        public ActionResult Cart()
        {
            if (UserInfo != null)
            {
                //查询购物车
                int page;
                int pageSize = 3;
                int count = cartbll.LoadEntities(c => c.UserId == UserInfo.Id).Count();

                int pageCount = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
                if (!int.TryParse(Request["page"], out page))
                {
                    page = 1;
                }
                page = page < 1 ? 1 : page;
                page = page > pageCount ? pageCount : page;
                List<Cart> cartmodel = new List<Cart>();
                if (count != 0)
                {
                    cartmodel = cartbll.LoadPageEntities<int>(page, pageSize, c => c.UserId == UserInfo.Id, c => c.Id, false).ToList();
                }
                //计算总金额
                var pricemodel = cartbll.LoadEntities(c => c.UserId == UserInfo.Id).ToList();
                decimal totalPrice = 0;
                foreach (var item in pricemodel)
                {
                    totalPrice = totalPrice + (int)item.Count * (decimal)item.Books.UnitPrice;
                }
                if (cartmodel.Count > 0)
                {
                    ViewData["cartlist"] = cartmodel;
                    ViewData["totalPrice"] = totalPrice.ToString("0.00");
                    ViewBag.PageIndex = page;
                    ViewBag.PageCount = pageCount;
                }
                else
                {
                    ViewData["cartlist"] = null;
                }
            }
            else
            {
                return Redirect("/Account/Index?redirect=" + HttpUtility.UrlEncode(Request.Url.ToString()));
            }
            return View();
        }

        //图书详情页添加图书到购物车
        public ActionResult AddCart()
        {
            if (UserInfo != null)
            {
                int id = Convert.ToInt32(Request["id"]);
                int count = Convert.ToInt32(Request["count"]);
                var bookmodel = bookbll.LoadEntities(c => c.Id == id).FirstOrDefault();
                if (bookmodel != null)
                {
                    //查询该用户购物车是否存在该图书
                    var cartid = cartbll.LoadEntities(c => c.UserId == UserInfo.Id && c.BookId == id).FirstOrDefault();
                    if (cartid != null)
                    {
                        cartid.Count = cartid.Count + count;
                        if (cartbll.UpdateEntity(cartid))
                        {
                            return Content("ok:亲，添加成功了哦！");
                        }
                        else
                        {
                            return Content("no:亲，添加失败了哦！");
                        }
                    }
                    else
                    {
                        Cart cart = new Cart();
                        cart.BookId = id;
                        cart.UserId = UserInfo.Id;
                        cart.Count = count;
                        if (cartbll.AddEntity(cart) != null)
                        {
                            return Content("ok:亲，添加成功了哦！");
                        }
                        else
                        {
                            return Content("no:亲，添加失败了哦！");
                        }
                    }
                }
                else
                {
                    return Content("no:亲，添加失败了哦！");
                }
            }
            else
            {
                return Content("nologin:亲，还没有登录哦！:" + HttpUtility.UrlEncode("/Home/ShopDetails?id="));
            }
        }

        //视频教程页添加图书到购物车
        public ActionResult AddCartTo()
        {
            if (UserInfo != null)
            {
                int id = Convert.ToInt32(Request["id"]);
                var bookmodel = bookbll.LoadEntities(c => c.Id == id).FirstOrDefault();
                if (bookmodel != null)
                {
                    //查询该用户购物车是否存在该图书
                    var cartid = cartbll.LoadEntities(c => c.UserId == UserInfo.Id && c.BookId == id).FirstOrDefault();
                    if (cartid != null)
                    {
                        cartid.Count = cartid.Count + 1;
                        if (cartbll.UpdateEntity(cartid))
                        {
                            return Content("ok:亲，添加成功了哦！");
                        }
                        else
                        {
                            return Content("no:亲，添加失败了哦！");
                        }
                    }
                    else
                    {
                        Cart cart = new Cart();
                        cart.BookId = id;
                        cart.UserId = UserInfo.Id;
                        cart.Count = 1;
                        if (cartbll.AddEntity(cart) != null)
                        {
                            return Content("ok:亲，添加成功了哦！");
                        }
                        else
                        {
                            return Content("no:亲，添加失败了哦！");
                        }
                    }
                }
                else
                {
                    return Content("no:亲，添加失败了哦！");
                }
            }
            else
            {
                return Content("nologin:亲，还没有登录哦！:" + HttpUtility.UrlEncode("/Home/ShopGifts?id="));
            }
        }
        //图书试读页添加图书到购物车
        public ActionResult AddCartReader()
        {
            if (UserInfo != null)
            {
                int id = Convert.ToInt32(Request["id"]);
                var bookmodel = bookbll.LoadEntities(c => c.Id == id).FirstOrDefault();
                if (bookmodel != null)
                {
                    //查询该用户购物车是否存在该图书
                    var cartid = cartbll.LoadEntities(c => c.UserId == UserInfo.Id && c.BookId == id).FirstOrDefault();
                    if (cartid != null)
                    {
                        cartid.Count = cartid.Count + 1;
                        if (cartbll.UpdateEntity(cartid))
                        {
                            return Content("ok:亲，添加成功了哦！");
                        }
                        else
                        {
                            return Content("no:亲，添加失败了哦！");
                        }
                    }
                    else
                    {
                        Cart cart = new Cart();
                        cart.BookId = id;
                        cart.UserId = UserInfo.Id;
                        cart.Count = 1;
                        if (cartbll.AddEntity(cart) != null)
                        {
                            return Content("ok:亲，添加成功了哦！");
                        }
                        else
                        {
                            return Content("no:亲，添加失败了哦！");
                        }
                    }
                }
                else
                {
                    return Content("no:亲，添加失败了哦！");
                }
            }
            else
            {
                return Content("nologin:亲，还没有登录哦！:" + HttpUtility.UrlEncode("/Home/ShopReaderList?id="));
            }
        }
        //布局页删除购物车中的商品
        public ActionResult DelCart()
        {
            if (Session["user"] != null)
            {
                int id = Request["id"].ToInt32();
                var delcart = cartbll.LoadEntities(c => c.Id == id).FirstOrDefault();
                if (delcart != null)
                {
                    if (cartbll.DeleteEntity(delcart))
                    {
                        return Content("ok");
                    }
                    else
                    {
                        return Content("no");
                    }
                }
                else
                {
                    return Content("no");
                }
            }
            else
            {
                return Content("nologin:亲，还没有登录哦！");
            }
        }
        //购买页面
        public ActionResult OrderInfo()
        {
            if (Session["user"] != null)
            {
                Users user = Session["user"] as Users;
                var userorder = userbll.LoadEntities(c => c.Id == user.Id).FirstOrDefault();
                string action = Request["action"];
                decimal totalPrice = 0;
                //从购物车中购买
                if (action == "cart")
                {
                    int page;
                    int pageSize = 3;
                    int count = cartbll.LoadEntities(c => c.UserId == user.Id).Count();

                    int pageCount = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
                    if (!int.TryParse(Request["page"], out page))
                    {
                        page = 1;
                    }
                    page = page < 1 ? 1 : page;
                    page = page > pageCount ? pageCount : page;
                    List<Cart> cartmodel = new List<Cart>();
                    if (count != 0)
                    {
                        cartmodel = cartbll.LoadPageEntities<int>(page, pageSize, c => c.UserId == user.Id, c => c.Id, false).ToList();
                    }
                    //计算总金额
                    var pricemodel = cartbll.LoadEntities(c => c.UserId == user.Id).ToList();
                    int ordercount = Convert.ToInt32(Request["count"]);
                    foreach (var item in pricemodel)
                    {
                        double discount = (double)item.Books.Discount / 100;
                        if (discount > 0 && discount < 1)
                        {
                            totalPrice = totalPrice + (int)item.Count * (decimal)item.Books.UnitPrice * (decimal)discount;
                        }
                        else
                        {
                            totalPrice = totalPrice + (int)item.Count * (decimal)item.Books.UnitPrice;
                        }
                    }
                    if (cartmodel != null)
                    {
                        ViewData["cartmodel"] = cartmodel;
                        ViewData["totalPrice"] = totalPrice.ToString("0.00");
                        ViewData["action"] = "cart";
                        ViewBag.PageIndex = page;
                        ViewBag.PageCount = pageCount;
                    }
                    else
                    {
                        ViewData["cartmodel"] = null;
                    }
                }
                //直接购买
                else
                {
                    int id = Convert.ToInt32(Request["id"]);
                    int ordercount = Convert.ToInt32(Request["count"]);
                    var bookmodel = bookbll.LoadEntities(c => c.Id == id).FirstOrDefault();
                    if (bookmodel != null)
                    {
                        ViewData["bookmodel"] = bookmodel;
                        ViewData["count"] = ordercount;
                        double discount = (double)bookmodel.Discount / 100;
                        if (discount > 0 && discount < 1)
                        {
                            ViewData["totalPrice"] = (bookmodel.UnitPrice * ordercount * (decimal)discount).ToString("0.00");
                        }
                        else
                        {
                            ViewData["totalPrice"] = (bookmodel.UnitPrice * ordercount).ToString("0.00");
                        }
                    }
                    else
                    {
                        return Redirect("/Home/Index");
                    }
                }
                ViewData["usermodel"] = userorder;
            }
            else
            {
                return Redirect("/Account/Index?redirect=" + HttpUtility.UrlEncode(Request.Url.ToString()));
            }
            return View();
        }
        //订单提交处理
        public void OrderForm()
        {
            if (UserInfo != null)
            {
                string orderid = DateTime.Now.ToString("yyyyMMddhhmmssfff") + UserInfo.Id;
                //收货地址
                string address = string.Format("收货人:{0},地址:{1},电话:{2}", Request["ordername"], Request["orderaddress"], Request["orderphone"]);
                int bookid = Convert.ToInt32(Request["bookid"]);
                var book = bookbll.LoadEntities(c => c.Id == bookid).FirstOrDefault();
                int count = Convert.ToInt32(Request["count"]);
                string action = Request["action"];
                if (action == "cart")//购物车购买
                {
                    //decimal totalprice = orderbll.CreateOrder(orderid, UserInfo.Id, address);
                    //遍历购物车
                    var carlist = cartbll.LoadEntities(c => c.UserId == UserInfo.Id).ToList();
                    if (carlist.Count > 0)
                    {
                        decimal totalmoney = 0;
                        foreach (var item in carlist)
                        {
                            double discount = (double)item.Books.Discount / 100;
                            if (discount > 0 && discount < 1)
                            {
                                totalmoney += item.Books.UnitPrice * item.Count * (decimal)discount;
                            }
                            else
                            {
                                totalmoney += item.Books.UnitPrice * item.Count;
                            }
                        }

                        Orders order = new Orders();
                        order.OrderId = orderid;
                        order.OrderDate = DateTime.Now;
                        order.UserId = UserInfo.Id;
                        order.PostAddress = address;
                        order.state = 1;
                        order.TotalPrice = totalmoney;

                        if (orderbll.AddEntity(order) != null)
                        {
                            foreach (var item in carlist)
                            {
                                OrderBook obook = new OrderBook();
                                obook.OrderID = orderid;
                                obook.BookID = item.BookId;
                                obook.Quantity = item.Count;
                                obook.UnitPrice = item.Books.UnitPrice;
                                orderbookbll.AddEntity(obook);

                                //添加推荐日志
                                logbll.AddUserOperationLog(UserInfo.Id, item.BookId, "购买图书", "时间:" + DateTime.Now + " 地点:订单详情", "", "");
                                //清除购物车
                                cartbll.DeleteEntity(item);
                            }
                        }
                        else
                        {
                            WebCommon.GoBack("亲，提交失败了哦");
                        }
                    }
                }
                else//直接购买
                {
                    Orders order = new Orders();
                    order.OrderId = orderid;
                    order.OrderDate = DateTime.Now;
                    order.UserId = UserInfo.Id;
                    order.PostAddress = address;
                    order.state = 1;
                    decimal totalPrice = 0;
                    double discount = (double)book.Discount / 100;
                    if (discount > 0 && discount < 1)
                    {
                        totalPrice = book.UnitPrice * count * (decimal)discount;
                    }
                    else
                    {
                        totalPrice = book.UnitPrice * count;
                    }
                    order.TotalPrice = totalPrice;

                    if (orderbll.AddEntity(order) != null)
                    {
                        OrderBook obook = new OrderBook();
                        obook.OrderID = orderid;
                        obook.BookID = bookid;
                        obook.Quantity = count;
                        obook.UnitPrice = book.UnitPrice;
                        orderbookbll.AddEntity(obook);

                        //添加推荐日志
                        logbll.AddUserOperationLog(UserInfo.Id, bookid, "购买图书", "时间:" + DateTime.Now + " 地点:订单详情", "", "");
                    }
                    else
                    {
                        WebCommon.GoBack("亲，提交失败了哦");
                    }
                }


                WebCommon.ShowUrl("亲，由于支付模块尚未实现，点确定则跳转订单页面并购买成功", "/Shop/Wishlist");
            }
            else
            {
                Response.Redirect("/Account/Index?redirect=" + HttpUtility.UrlEncode(Request.Url.ToString()));
            }
        }
        //订单记录页面
        public ActionResult Wishlist()
        {
            if (UserInfo != null)
            {
                //查询购物车
                int page;
                int pageSize = 3;
                int count = orderbll.LoadEntities(c => c.UserId == UserInfo.Id).Count();
                int pageCount = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
                if (!int.TryParse(Request["page"], out page))
                {
                    page = 1;
                }
                page = page < 1 ? 1 : page;
                page = page > pageCount ? pageCount : page;
                List<Orders> cartmodel = new List<Orders>();
                if (count != 0)
                {
                    cartmodel = orderbll.LoadPageEntities<DateTime>(page, pageSize, c => c.UserId == UserInfo.Id, c => c.OrderDate, false).ToList();
                }
                if (cartmodel.Count > 0)
                {
                    ViewData["orderlist"] = cartmodel;
                    ViewBag.PageIndex = page;
                    ViewBag.PageCount = pageCount;
                }
                else
                {
                    ViewData["orderlist"] = null;
                }
            }
            else
            {
                return Redirect("/Account/Index?redirect=" + HttpUtility.UrlEncode(Request.Url.ToString()));
            }
            return View();
        }
        //订单详细页面
        public ActionResult WishlistDetail()
        {
            if (UserInfo != null)
            {
                string orderid = Request["orderid"];
                if (string.IsNullOrEmpty(orderid))
                {
                    return RedirectToAction("Wishlist");
                }
                //查询购物车
                int page;
                int pageSize = 3;
                int count = orderbookbll.LoadEntities(c => c.OrderID == orderid&&c.Orders.UserId==UserInfo.Id).Count();
                int pageCount = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
                if (!int.TryParse(Request["page"], out page))
                {
                    page = 1;
                }
                page = page < 1 ? 1 : page;
                page = page > pageCount ? pageCount : page;
                List<OrderBook> cartmodel = new List<OrderBook>();
                if (count != 0)
                {
                    cartmodel = orderbookbll.LoadPageEntities<DateTime>(page, pageSize, c => c.OrderID == orderid && c.Orders.UserId == UserInfo.Id, c => c.Orders.OrderDate, false).ToList();
                }
                if (cartmodel.Count > 0)
                {
                    ViewData["orderlist"] = cartmodel;
                    ViewBag.PageIndex = page;
                    ViewBag.PageCount = pageCount;
                    ViewBag.OrderID = orderid;
                }
                else
                {
                    ViewData["orderlist"] = null;
                }
            }
            else
            {
                return Redirect("/Account/Index?redirect=" + HttpUtility.UrlEncode(Request.Url.ToString()));
            }
            return View();
        }
        //删除订单
        public void DelOrder()
        {
            string id = Request["id"];
            var orderbook = orderbookbll.LoadEntities(c => c.OrderID == id).ToList();
            List<int> list = new List<int>();
            foreach (var item in orderbook)
            {
                list.Add(item.Id);
            }
            var order = orderbll.LoadEntities(c => c.OrderId == id).FirstOrDefault();
            if (orderbookbll.DeleteEntities(list) && orderbll.DeleteEntity(order))
            {
                WebCommon.ShowUrl("删除成功", "/Shop/Wishlist");
            }
            else
            {
                WebCommon.GoBack("删除失败");
            }
        }
        //删除订单详细
        public void DelOrderDetail()
        {
            int id = Convert.ToInt32(Request["id"]);
            var orderbook = orderbookbll.LoadEntities(c => c.Id == id).FirstOrDefault();

            if (orderbook != null && orderbookbll.DeleteEntity(orderbook))
            {
                WebCommon.ShowUrl("删除成功", "/Shop/WishlistDetail?orderid=" + orderbook.OrderID);
            }
            else
            {
                WebCommon.GoBack("删除失败");
            }
        }
        //添加评论
        public ActionResult AddComment()
        {
            int bookid = Convert.ToInt32(Request["bookid"]);
            if (UserInfo != null)
            {
                var bookmodel = bookbll.LoadEntities(c => c.Id == bookid).FirstOrDefault();
                if (bookmodel == null)
                {
                    return Content("no:亲，该图书不存在或已被删除！");
                }
                //首先查询 是否有资格评论 必须购买过才能评论
                var isorder = orderbookbll.LoadEntities(c => c.Orders.UserId == UserInfo.Id && c.BookID == bookid).Count();
                if (isorder > 0)
                {
                    var iscomment = commentbll.LoadEntities(c => c.UserId == UserInfo.Id && c.BookId == bookid).Count();

                    if (iscomment < isorder)//可以评论
                    {
                        string rating = Request["rating"];

                        if (string.IsNullOrEmpty(rating))
                        {
                            return Content("no:亲，请您先为该书评分，谢谢！");
                        }
                        else
                        {
                            //查询是否评过分
                            var israting = ratingbll.LoadEntities(c => c.userID == UserInfo.Id && c.bookID == bookid).FirstOrDefault();
                            if (israting != null)
                            {
                                israting.stars = Convert.ToDouble(rating);
                                israting.updateTime = DateTime.Now;
                                ratingbll.UpdateEntity(israting);
                            }
                            else
                            {
                                recommend_rating ratingmodel = new recommend_rating();
                                ratingmodel.bookID = bookid;
                                ratingmodel.userID = UserInfo.Id;
                                ratingmodel.stars = Convert.ToDouble(rating);
                                ratingmodel.addTime = DateTime.Now;
                                ratingbll.AddEntity(ratingmodel);
                            }
                        }
                        string msg = Request["Msg"];
                        if (string.IsNullOrEmpty(msg))
                        {
                            return Content("no:亲，您评论的内容不能为空哦！");
                        }
                        msg = msg.Replace("<", "&lt;").Replace(">", "&gt;");//替换危险字符
                        if (abll.CheckBanndWord(msg))//过滤
                        {
                            return Content("no:亲，您评论的内容含有禁用词哦！");
                        }
                        else if (abll.CheckModWord(msg))
                        {
                            return Content("no:亲，您评论的内容含有审查词哦！");
                        }
                        else
                        {
                            BookComment model = new BookComment();
                            model.BookId = bookid;
                            model.UserId = UserInfo.Id;
                            model.Msg = msg;
                            model.RegTime = DateTime.Now;
                            commentbll.AddEntity(model);

                            //更新图书的总体评分
                            var ratinglist = ratingbll.LoadEntities(c => c.bookID == bookid);
                            double pinfen = 0;
                            if (ratinglist.Count() > 0)
                            {
                                pinfen = Math.Round((double)ratinglist.Sum(c => c.stars) / (double)ratinglist.Count(), 1);
                            }
                            bookmodel.rating = pinfen;
                            bookbll.UpdateEntity(bookmodel);

                            //添加推荐日志
                            logbll.AddUserOperationLog(UserInfo.Id, bookid, "商品评论", "时间:" + DateTime.Now + " 地点:商品详情", "", "");

                            return Content("ok:评论成功！");
                        }
                    }
                    else
                    {
                        return Content("no:亲，您需要再次购买才能评论哦！:");
                    }
                }
                else
                {
                    return Content("no:亲，您还没有购买此商品哦！:");
                }
            }
            else
            {
                return Content("nologin:亲，登录以后才能评论哦！:" + HttpUtility.UrlEncode("/Home/ShopDetails?id=" + bookid));
            }
        }
    }
}
