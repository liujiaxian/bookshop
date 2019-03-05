using BookShop.BLL;
using BookShop.IBLL;
using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Common;
using BookShop.WebApp.Models;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;
using NReco.CF.Taste.Impl.Model.File;
using NReco.CF.Taste.Impl.Model;
using NReco.CF.Taste.Impl.Similarity;
using NReco.CF.Taste.Impl.Neighborhood;
using NReco.CF.Taste.Impl.Recommender;
using NReco.CF.Taste.Model;
using System.Collections;

namespace BookShop.WebApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        ISettingsBLL settingbll = new SettingsBLL();
        IBooksBLL booksbll = new BooksBLL();
        ICategoriesBLL catbll = new CategoriesBLL();
        IBookCommentBLL commentbll = new BookCommentBLL();
        IPdfReaderBLL pdfbll = new PdfReaderBLL();
        IContactBLL contactbll = new ContactBLL();
        Irecommend_ratingBLL ratingbll = new BLL.recommend_ratingBLL();
        Irecommend_logBLL logbll = new BLL.recommend_logBLL();
        IOrdersBLL orderbll = new BLL.OrdersBLL();
        IOrderBookBLL orderbookbll = new BLL.OrderBookBLL();
        IHotCountBLL hotbll = new BLL.HotCountBLL();

        public ActionResult Index()
        {
            //查询广告
            var setting = settingbll.LoadEntities(c => c.key == "广告语").FirstOrDefault();
            var settingimg = settingbll.LoadEntities(c => c.key == "广告图").FirstOrDefault();
            string[] gg = setting.value.Split('-');
            var bookid = settingimg.bookid;
            ViewData["gg"] = gg;
            ViewData["ggbookid"] = bookid;
            ViewData["img"] = settingimg.value;
            //查询推荐图书


            ViewData["books"] = RecommendBooks(1, 6, 6);

            //查询最低折扣商品
            ViewData["lowbooks"] = booksbll.LoadEntities(c => true).OrderBy(c => c.Discount).Skip(0).Take(6).ToList();
            //查询热门商品
            ViewData["hotbooks"] = booksbll.LoadEntities(c => true).OrderBy(c => c.hotcount).Skip(0).Take(6).ToList();
            //查询光盘图书
            ViewData["drivebooks"] = booksbll.LoadEntities(c => c.CategoryId == 83).OrderByDescending(c => c.Id).Skip(0).Take(6).ToList();
            //查询视频教程图书
            ViewData["videobooks"] = booksbll.LoadEntities(c => c.CategoryId == 84).OrderByDescending(c => c.Id).Skip(0).Take(6).ToList();
            //查询阅读图书
            ViewData["readerbooks"] = booksbll.LoadEntities(c => c.CategoryId == 85).OrderByDescending(c => c.Id).Skip(0).Take(6).ToList();
            return View();
        }
        //商品列表
        public ActionResult ShopList()
        {
            int id = Convert.ToInt32(Request["id"]);
            int page;
            int pageSize = 12;
            int count = 0;
            if (!int.TryParse(Request["page"], out page))
            {
                page = 1;
            }
            page = page < 1 ? 1 : page;


            List<Model.Books> books = null;
            string catname = "";
            switch (id)
            {
                case -1://推荐
                    books = RecommendBooks(page, pageSize, pageSize * 4);
                    count = RecommendBooks(page, pageSize, pageSize * 4).Count;
                    catname = "推荐";
                    break;
                case -2://热门
                    books = booksbll.LoadPageEntities<int>(page, pageSize, c => true, c => (int)c.hotcount, false).ToList();
                    count = pageSize * 4;
                    catname = "热门";
                    break;
                case -3://低价
                    books = booksbll.LoadPageEntities<int>(page, pageSize, c => true, c => c.Discount, true).ToList();
                    count = pageSize * 4;
                    catname = "低价";
                    break;
                default:
                    books = booksbll.LoadPageEntities<int>(page, pageSize, c => c.CategoryId == id, c => c.Id, false).ToList();
                    count = booksbll.LoadEntities(c => c.CategoryId == id).Count();
                    catname = catbll.LoadEntities(c => c.Id == id).FirstOrDefault().Name;
                    break;
            }
            int pageCount = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
            page = page > pageCount ? pageCount : page;


            //查询分类名称
            ViewData["catname"] = catname;
            ViewData["booklist"] = books;
            ViewBag.PageIndex = page;
            ViewBag.PageCount = pageCount;
            ViewBag.Id = id;
            return View();
        }
        //商品详情
        [ValidateInput(false)]
        public ActionResult ShopDetails()
        {
            int id = Convert.ToInt32(Request["id"]);
            var bookmodel = booksbll.LoadEntities(c => c.Id == id).FirstOrDefault();
            if (bookmodel != null)
            {
                //查找相关图书
                var combook = booksbll.LoadEntities(c => c.Categories.PId == bookmodel.Categories.PId && c.Id != id).OrderByDescending(c => c.Id).Skip<Books>(0).Take<Books>(12).ToList();
                //查看评论内容
                int page;
                if (!int.TryParse(Request["page"], out page))
                {
                    page = 1;
                }
                page = page < 1 ? 1 : page;
                int pagesize = 5;
                int count = commentbll.LoadEntities(c => c.BookId == id).Count();
                if (count > 0)
                {
                    ViewBag.TotalCount = count;
                }
                int pageCount = commentbll.GetPageCount(pagesize, id);
                if (pageCount > 0)
                {
                    page = page > pageCount ? pageCount : page;
                    var commentmodel = commentbll.LoadPageEntities<int>(page, pagesize, c => c.BookId == id, c => c.Id, false).ToList();
                    ViewData["comment"] = commentmodel;
                    ViewBag.PageCount = pageCount;
                }
                ViewData["bookmodel"] = bookmodel;

                #region 推荐
                List<Books> newblist = new List<Books>();
                //冷启动
                //首先查找购买此图书的用户还购买过的商品
                var order = orderbookbll.LoadEntities(c => c.BookID == id).GroupBy(c => c.Orders.UserId).ToList();
                foreach (var item in order)
                {
                    var blist = orderbookbll.LoadEntities(c => c.Orders.UserId == item.Key && c.BookID != id).GroupBy(c => c.BookID).ToList();
                    foreach (var citem in blist)
                    {
                        //过滤自己购买过的图书
                        if (Session["user"] != null)
                        {
                            Users user = Session["user"] as Users;
                            var myself = orderbookbll.LoadEntities(c => c.BookID == citem.Key && c.Orders.UserId == user.Id).FirstOrDefault();
                            if (myself != null)
                            {
                                continue;
                            }
                        }
                        var bmodel = booksbll.LoadEntities(c => c.Id == citem.Key).FirstOrDefault();
                        newblist.Add(bmodel);
                    }
                }
                #endregion

                ViewData["combook"] = newblist.OrderByDescending(c => c.rating).Skip(0).Take(12).Count() <= 0 ? combook : newblist.OrderByDescending(c => c.rating).Skip(0).Take(12).ToList();

                ViewBag.PageIndex = page;
                ViewBag.Id = id;
            }
            else
            {
                Response.Redirect("/Home/Index");
            }

            if (Session["user"] != null)
            {
                Users user = Session["user"] as Users;
                logbll.AddUserOperationLog(user.Id, id, "浏览", "时间:" + DateTime.Now + " 地点:商品详情", "", "");
            }


            #region 浏览记录
            //记录ip地址
            string ip = WebCommon.GetIPAddress();

            //查询是否存在此数据
            var isip = hotbll.LoadEntities(c => c.bookID == id && c.ipAddress == ip).FirstOrDefault();
            if (isip == null)
            {
                HotCount hot = new HotCount();
                hot.bookID = id;
                hot.ipAddress = ip;
                hot.addTime = DateTime.Now;
                hotbll.AddEntity(hot);

                var count = hotbll.LoadEntities(c => c.bookID == id).Count();
                bookmodel.hotcount = count;
                booksbll.UpdateEntity(bookmodel);
            }
            #endregion

            #region 最近浏览cookie
            if (Request.Cookies["bookrecord"] != null && Request.Cookies["bookrecord"].Value.IndexOf(',') != -1)
            {
                string[] str = Request.Cookies["bookrecord"].Value.Split(',');
                bool exists = ((IList)str).Contains(id.ToString());
                if (!exists)//
                {
                    string strid = id + "," + Request.Cookies["bookrecord"].Value;
                    HttpCookie ck1 = new HttpCookie("bookrecord", strid);
                    ck1.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(ck1);
                }
            }
            else
            {
                string bookstr = id + ",";
                HttpCookie ck1 = new HttpCookie("bookrecord", bookstr);
                ck1.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(ck1);
            }


            #endregion

            return View();
        }

        //视频教程
        public ActionResult ShopGifts()
        {
            int id = Convert.ToInt32(Request["id"]);
            var count = booksbll.LoadEntities(c => c.CategoryId == id).Count();
            if (count > 0)
            {
                int page;
                int pageSize = 6;
                int pageCount = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
                if (!int.TryParse(Request["page"], out page))
                {
                    page = 1;
                }
                page = page < 1 ? 1 : page;
                page = page > pageCount ? pageCount : page;
                var books = booksbll.LoadPageEntities<int>(page, pageSize, c => c.CategoryId == id, c => c.Id, false).ToList();
                ViewData["catbooks"] = books;
                ViewBag.PageIndex = page;
                ViewBag.PageCount = pageCount;
                ViewBag.Id = id;
                ViewData["footer"] = "<div class='clearfix'></div></div>";
                ViewData["header"] = "<div class='col-md1'>";
                return View();
            }
            else
            {
                return Content("<script type='text/javascript'>alert('亲，还没有此类商品哦！');window.location.href='/Home/Index'</script>");
            }
        }

        //在线阅读
        public ActionResult ShopReaderList()
        {
            int id = Convert.ToInt32(Request["id"]);
            var count = booksbll.LoadEntities(c => c.CategoryId == id).Count();
            if (count > 0)
            {
                int page;
                int pageSize = 6;
                int pageCount = Convert.ToInt32(Math.Ceiling((double)count / pageSize));
                if (!int.TryParse(Request["page"], out page))
                {
                    page = 1;
                }
                page = page < 1 ? 1 : page;
                page = page > pageCount ? pageCount : page;
                var books = booksbll.LoadPageEntities<int>(page, pageSize, c => c.CategoryId == id, c => c.Id, false).ToList();
                ViewData["catbooks"] = books;
                ViewBag.PageIndex = page;
                ViewBag.PageCount = pageCount;
                ViewBag.Id = id;
                ViewData["footer"] = "<div class='clearfix'></div></div>";
                ViewData["header"] = "<div class='col-md1'>";
                return View();
            }
            else
            {
                return Content("<script type='text/javascript'>alert('亲，还没有此类商品哦！');window.location.href='/Home/Index'</script>");
            }
        }
        public ActionResult CheckReader()
        {
            if (Session["user"] != null)
            {
                Users user = Session["user"] as Users;
                int id = Convert.ToInt32(Request["id"]);
                var bookdr = booksbll.LoadEntities(c => c.Id == id).FirstOrDefault();
                if (bookdr != null)
                {
                    if (!string.IsNullOrEmpty(bookdr.Pdfname))
                    {
                        Guid guid = Guid.NewGuid();
                        string token = guid.ToString().Md5();
                        var pdfid = pdfbll.LoadEntities(c => c.UserId == user.Id && c.BookId == id).FirstOrDefault();
                        if (pdfid != null)
                        {
                            pdfid.Token = token;
                            if (pdfbll.UpdateEntity(pdfid))
                            {
                                if (pdfid.ReadTime.AddDays(3) < DateTime.Now)
                                {
                                    return Content("no:亲，您的试读时间已结束了哦！");
                                }
                                else
                                {
                                    TimeSpan time = pdfid.ReadTime.AddDays(3) - DateTime.Now;
                                    return Content("ok:亲，您的试读时间剩余" + time.Days + "天" + time.Hours + "时" + time.Minutes + "分！:" + token);
                                }
                            }
                            else
                            {
                                return Content("no:亲，试读失败了哦！");
                            }
                        }
                        else
                        {
                            PdfReader pdf = new PdfReader();
                            pdf.UserId = user.Id;
                            pdf.BookId = id;
                            pdf.Token = token;
                            pdf.ReadTime = DateTime.Now;
                            if (pdfbll.AddEntity(pdf) != null)
                            {
                                return Content("ok:亲，试读只有三天哦！:" + token);
                            }
                            else
                            {
                                return Content("no:亲，试读失败了哦！");
                            }
                        }
                    }
                    else
                    {
                        return Content("no:亲，系统暂无此图书哦！");
                    }
                }
                else
                {
                    return Content("no:亲，参数错误哦！");
                }
            }
            else
            {
                return Content("nologin:亲，登录后才能试读哦！:" + HttpUtility.UrlEncode("/Home/ShopReaderList?id=85"));
            }
        }
        //调用插件pdf
        public ActionResult BookReader()
        {
            if (Session["user"] != null)
            {
                Users user = Session["user"] as Users;
                string token = Request["token"];
                var pdf = pdfbll.LoadEntities(c => c.UserId == user.Id && c.Token == token).FirstOrDefault();
                if (pdf != null)
                {
                    string host = Request.Url.Scheme + "://" + Request.Url.Authority;
                    ViewData["pdf"] = host + "/Pdf/" + pdf.Books.Pdfname;
                    return View();
                }
                else
                {
                    return Content("<script type='text/javascript'>alert('亲，参数错误哦！');window.location.href='/Home/ShopReaderList?id=85'</script>");
                }
            }
            else
            {
                return Content("<script type='text/javascript'>alert('亲，还没有登录哦！');window.location.href='/Account/Index?redirect='" + HttpUtility.UrlEncode(Request.Url.ToString()) + "</script>");
            }
        }
        public ActionResult ShowMsg()
        {
            string msg = string.IsNullOrEmpty(Request["Msg"]) ? "暂无信息" : Server.UrlDecode(Request["Msg"]);
            string txt = string.IsNullOrEmpty(Request["Txt"]) ? "首页" : Server.UrlDecode(Request["Txt"]);
            string url = string.IsNullOrEmpty(Request["Url"]) ? "/Home/Index" : Request["Url"];
            ViewBag.msg = msg;
            ViewBag.txt = txt;
            ViewBag.url = url;
            return View();
        }
        //联系我们
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            Contact con = new Contact();
            con.Name = contact.Name;
            con.Email = contact.Email;
            con.Content = contact.Content;
            con.Regtime = DateTime.Now;
            Contact model = contactbll.AddEntity(con);
            if (model != null)
            {
                WebCommon.Show("提交成功");
            }
            else
            {
                WebCommon.Show("提交失败");
            }
            return View("Contact");
        }
        //错误页面404

        public ActionResult Error()
        {
            var list = booksbll.LoadEntities(c => true).ToList();
            int[] arr = new int[] { 22, 26, 29, 49, 55, 57, 58,59, 64, 65, 66, 67, 69, 70, 71, 74, 75, 76, 77, 89, 91, 92, 94, 95, 96, 97, 98, 100, 101 };
            double[] rat = new double[] { 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 };

            int [] book = new int [list.Count];
            foreach (var item in list)
            {
                book[list.IndexOf(item)] = item.Id;
            }

            Random r = new Random();


            for (int i = 0; i < book.Length; i++)
            {
                int bid = book[i];
                int uid = arr[r.Next(arr.Length)];
                double rid = rat[r.Next(rat.Length)];

                recommend_rating rmodel = new recommend_rating();
                rmodel.bookID = bid;
                rmodel.userID = uid;
                rmodel.stars = rid;
                rmodel.addTime = DateTime.Now;
                ratingbll.AddEntity(rmodel);
            }

            //foreach (var item in list)
            //{
            //    int fi1 = arr[r.Next(arr.Length)];
            //    item.Discount = fi1;
            //    booksbll.UpdateEntity(item);
            //}
            return Content("ok");
        }

        public ActionResult Test1()
        {
            var list = ratingbll.LoadEntities(c => true).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                //需要过滤 取平均值
                sb.Append(item.userID + "\t" + item.bookID + "\t" + item.stars + "\t" + WebCommon.DateTimeToUnixTimestamp(Convert.ToDateTime(item.addTime)) + "\r\n");
            }
            System.IO.File.WriteAllText(Server.MapPath("/data/ratings.dat"), sb.ToString());//写入文件
            return Content("ok");
        }


        static IDataModel dataModel;
        public ActionResult Test()
        {


            //构建用户行为数组

            string filmIdsJson = "[6090,5461,5426]";

            var filmIds = (new JavaScriptSerializer()).Deserialize<long[]>(filmIdsJson);
            var pathToDataFile =
                    Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "data/ratings.dat");

            if (dataModel == null)
            {
                dataModel = new FileDataModel(pathToDataFile, false, FileDataModel.DEFAULT_MIN_RELOAD_INTERVAL_MS, false);
            }

            var plusAnonymModel = new PlusAnonymousUserDataModel(dataModel);
            var prefArr = new GenericUserPreferenceArray(filmIds.Length);
            prefArr.SetUserID(0, PlusAnonymousUserDataModel.TEMP_USER_ID);
            for (int i = 0; i < filmIds.Length; i++)
            {
                prefArr.SetItemID(i, filmIds[i]);
                prefArr.SetValue(i, 5); // lets assume max rating
            }
            plusAnonymModel.SetTempPrefs(prefArr);

            var similarity = new LogLikelihoodSimilarity(plusAnonymModel);
            var neighborhood = new NearestNUserNeighborhood(15, similarity, plusAnonymModel);
            var recommender = new GenericUserBasedRecommender(plusAnonymModel, neighborhood, similarity);
            var recommendedItems = recommender.Recommend(PlusAnonymousUserDataModel.TEMP_USER_ID, 5, null);

            return Json(recommendedItems.Select(ri => new Dictionary<string, object>() {
                {"film_id", ri.GetItemID() },
                {"rating", ri.GetValue() },
            }).ToArray(), JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 推荐
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="showCount">显示数量</param>
        /// <returns></returns>
        public List<Books> RecommendBooks(int pageIndex, int pageSize, int showCount)
        {
            #region 推荐
            List<Books> books = null;
            if (Session["user"] != null)
            {
                Users user = Session["user"] as Users;

                #region 构建用户行为数组
                var loglist = logbll.LoadEntities(c => c.userID == user.Id).ToList();
                StringBuilder sb = new StringBuilder();
                if (loglist.Count > 0)
                {
                    sb.Append("[");
                    int j = 0;
                    foreach (var item in loglist)
                    {
                        j++;
                        sb.Append(item.itemID.ToString());
                        if (j != loglist.Count)
                        {
                            sb.Append(",");
                        }
                    }
                    sb.Append("]");
                }
                #endregion

                if (string.IsNullOrEmpty(sb.ToString()))
                {
                    //冷启动
                    books = booksbll.LoadEntities(c => true).OrderByDescending(c => c.rating).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var filmIds = (new JavaScriptSerializer()).Deserialize<long[]>(sb.ToString());

                    var logmodel = settingbll.LoadEntities(c => c.id == 16).FirstOrDefault();
                    string path = "";
                    if (logmodel != null && logmodel.value == "true")
                    {
                        path = "data/ratings1.dat";
                    }
                    else
                    {
                        path = "data/ratings.dat";
                    }

                    var pathToDataFile =
                            Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, path);

                    if (dataModel == null)
                    {
                        dataModel = new FileDataModel(pathToDataFile, false, FileDataModel.DEFAULT_MIN_RELOAD_INTERVAL_MS, false);
                    }

                    var plusAnonymModel = new PlusAnonymousUserDataModel(dataModel);
                    var prefArr = new GenericUserPreferenceArray(filmIds.Length);
                    prefArr.SetUserID(0, PlusAnonymousUserDataModel.TEMP_USER_ID);
                    for (int i = 0; i < filmIds.Length; i++)
                    {
                        prefArr.SetItemID(i, filmIds[i]);
                        prefArr.SetValue(i, 5); // lets assume max rating
                    }
                    plusAnonymModel.SetTempPrefs(prefArr);

                    var similarity = new LogLikelihoodSimilarity(plusAnonymModel);
                    var neighborhood = new NearestNUserNeighborhood(15, similarity, plusAnonymModel);
                    var recommender = new GenericUserBasedRecommender(plusAnonymModel, neighborhood, similarity);
                    var recommendedItems = recommender.Recommend(PlusAnonymousUserDataModel.TEMP_USER_ID, showCount, null);
                    List<Books> newbooks = new List<Books>();
                    foreach (var item in recommendedItems)
                    {
                        int bid = Convert.ToInt32(item.GetItemID());
                        newbooks.Add(booksbll.LoadEntities(c => c.Id == bid).FirstOrDefault());
                    }

                    books = newbooks.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            else //不推荐
            {
                books = booksbll.LoadEntities(c => true).OrderByDescending(c => c.rating).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            #endregion

            return books.Count() <= 0 ? booksbll.LoadEntities(c => true).OrderByDescending(c => c.rating).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList() : books;
        }

    }
}
