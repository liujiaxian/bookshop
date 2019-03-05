using BookShop.Model;
using BookShop.Model.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Common;
using BookShop.WebApp.Models;

namespace BookShop.WebApp.Areas.Admin.Controllers
{
    public class AdminHomeController : AdminBaseController
    {
        //
        // GET: /Admin/AdminHome/
        IBLL.IUsersBLL userbll = new BLL.UsersBLL();
        IBLL.IRoleInfoBLL rolebll = new BLL.RoleInfoBLL();
        IBLL.IUserStatesBLL statebll = new BLL.UserStatesBLL();
        IBLL.IActionInfoBLL actionbll = new BLL.ActionInfoBLL();
        IBLL.ISettingsBLL settingsbll = new BLL.SettingsBLL();
        IBLL.ICategoriesBLL categoriesbll = new BLL.CategoriesBLL();
        IBLL.IBooksBLL booksbll = new BLL.BooksBLL();
        IBLL.IPublishersBLL publishbll = new BLL.PublishersBLL();
        IBLL.IOrderBookBLL orderbookbll = new BLL.OrderBookBLL();
        IBLL.IOrdersBLL ordersbll = new BLL.OrdersBLL();
        IBLL.ICartBLL cartbll = new BLL.CartBLL();
        IBLL.IBookCommentBLL commentbll = new BLL.BookCommentBLL();
        IBLL.IArticelWordsBLL abll = new BLL.ArticelWordsBLL();
        public ActionResult Index()
        {
            //直接调用父类中的属性
            ViewData["name"] = UserInfo.LoginId;
            return View();
        }
        #region 用户管理

        #region 页面展示
        public ActionResult ShowUserInfo()
        {
            //查询所有角色
            var userrole = rolebll.LoadEntities(c => true).ToList();
            ViewData["rolelist"] = userrole;
            //查询所有状态
            var userstate = statebll.LoadEntities(c => true).ToList();
            ViewData["statelist"] = userstate;
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetUserInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            string name = Request["SearchName"];
            string email = Request["SearchEmail"];
            string role = Request["SearchRole"];
            string state = Request["SearchState"];
            int totalCount = 0;
            //搜索
            UserInfoParams userinfoparams = new UserInfoParams()
            {
                UserName = name,
                UserEmail = email,
                UserRole = role,
                UserState = state,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };
            var userinfo = userbll.LoadSearchParams(userinfoparams);
            var rows = from u in userinfo
                       select new { Id = u.Id, LoginId = u.LoginId, LoginPwd = u.LoginPwd, Name = u.Name, Address = u.Address, Phone = u.Phone, Mail = u.Mail, UserRole = u.RoleInfo.RoleName, UserState = u.UserStates.Name, Money = u.Money, RegTime = u.RegTime };
            return Json(new { total = userinfoparams.TotalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加数据
        public ActionResult AddUserInfo(Users user)
        {
            string userrole = Request["UserRoleId"];
            var roleid = rolebll.LoadEntities(c => c.RoleName == userrole).FirstOrDefault();
            string userstate = Request["UserStateId"];
            var stateid = statebll.LoadEntities(c => c.Name == userstate).FirstOrDefault();
            Users userinfo = new Users();
            userinfo.LoginId = user.LoginId;
            userinfo.LoginPwd = user.LoginPwd.Md5();
            userinfo.Name = user.Name;
            userinfo.Address = user.Address;
            userinfo.Phone = user.Phone;
            userinfo.Mail = user.Mail;
            userinfo.UserRoleId = roleid.RoleId;
            userinfo.UserStateId = stateid.Id;
            userinfo.Money = user.Money;
            userinfo.RegTime = DateTime.Now;
            if (userbll.AddEntity(userinfo) != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 修改数据
        public ActionResult EditUserInfo(Users user)
        {
            int id = Convert.ToInt32(Request["Id"]);
            var userinfo = userbll.LoadEntities(c => c.Id == id).FirstOrDefault();
            if (userinfo != null)
            {
                string userrole = Request["UserRoleId"];
                var roleid = rolebll.LoadEntities(c => c.RoleName == userrole).FirstOrDefault();
                string userstate = Request["UserStateId"];
                var stateid = statebll.LoadEntities(c => c.Name == userstate).FirstOrDefault();
                userinfo.LoginId = user.LoginId;
                userinfo.LoginPwd = user.LoginPwd;
                userinfo.Name = user.Name;
                userinfo.Address = user.Address;
                userinfo.Phone = user.Phone;
                userinfo.Mail = user.Mail;
                userinfo.UserRoleId = roleid.RoleId;
                userinfo.UserStateId = stateid.Id;
                userinfo.Money = user.Money;
                if (userbll.UpdateEntity(userinfo))
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
        #endregion

        #region 删除数据
        public ActionResult DeleteUserInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (userbll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #endregion

        #region 角色管理

        #region 页面展示
        public ActionResult ShowRoleInfo()
        {
            //查询所有权限
            var roleaction = actionbll.LoadEntities(c => true).ToList();
            ViewData["actionlist"] = roleaction;
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetRoleInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            string rolename = Request["SearchRoleName"];
            int totalCount = 0;
            RoleInfoParams roleinfoparams = new RoleInfoParams()
            {
                UserRole = rolename,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            //var userinfo = userbll.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => true, c => c.Id, true);
            var roleinfo = rolebll.LoadSearchParams(roleinfoparams);
            var rows = from u in roleinfo
                       select new { RoleId = u.RoleId, RoleName = u.RoleName, RoleDesc = u.RoleDesc, ActionName = u.ActionInfo.ActionName };
            return Json(new { total = roleinfoparams.TotalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加数据
        public ActionResult AddRoleInfo(RoleInfo role)
        {
            string roleaction = Request["ActionList"];
            var actionid = actionbll.LoadEntities(c => c.ActionName == roleaction).FirstOrDefault();
            RoleInfo roleinfo = new RoleInfo();
            roleinfo.RoleName = role.RoleName;
            roleinfo.RoleDesc = role.RoleDesc;
            roleinfo.ActionId = actionid.ActionId;
            if (rolebll.AddEntity(roleinfo) != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 修改数据
        public ActionResult EditRoleInfo(RoleInfo role)
        {
            int id = Convert.ToInt32(Request["Id"]);
            var roleinfo = rolebll.LoadEntities(c => c.RoleId == id).FirstOrDefault();
            if (roleinfo != null)
            {
                string roleaction = Request["ActionList"];
                var actionid = actionbll.LoadEntities(c => c.ActionName == roleaction).FirstOrDefault();
                roleinfo.RoleName = role.RoleName;
                roleinfo.RoleDesc = role.RoleDesc;
                roleinfo.ActionId = actionid.ActionId;
                if (rolebll.UpdateEntity(roleinfo))
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
        #endregion

        #region 删除数据
        public ActionResult DeleteRoleInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (rolebll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #endregion

        #region 权限管理
        #region 页面展示
        public ActionResult ShowActionInfo()
        {
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetActionInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            string actionname = Request["SearchActionName"];
            int totalCount = 0;
            ActionInfoParams actionparams = new ActionInfoParams()
            {
                ActionName = actionname,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            //var userinfo = userbll.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => true, c => c.Id, true);
            var actioninfo = actionbll.LoadSearchParams(actionparams);
            var rows = from u in actioninfo
                       select new { ActionId = u.ActionId, ActionName = u.ActionName, ActionUrl = u.ActionUrl, Remark = u.Remark };
            return Json(new { total = actionparams.TotalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加数据
        public ActionResult AddActionInfo(ActionInfo actions)
        {
            ActionInfo actioninfo = new ActionInfo();
            actioninfo.ActionName = actions.ActionName;
            actioninfo.ActionUrl = actions.ActionUrl;
            actioninfo.Remark = actions.Remark;
            if (actionbll.AddEntity(actioninfo) != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 修改数据
        public ActionResult EditActionInfo(ActionInfo actionupdate)
        {
            int id = Convert.ToInt32(Request["Id"]);
            var actioninfo = actionbll.LoadEntities(c => c.ActionId == id).FirstOrDefault();
            if (actioninfo != null)
            {
                actioninfo.ActionName = actionupdate.ActionName;
                actioninfo.ActionUrl = actionupdate.ActionUrl;
                actioninfo.Remark = actionupdate.Remark;
                if (actionbll.UpdateEntity(actioninfo))
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
        #endregion

        #region 删除数据
        public ActionResult DeleteActionInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (actionbll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
        #endregion

        #region 配置管理

        #region 页面展示
        public ActionResult ShowSettingsInfo()
        {
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetSettingsInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            string settingskey = Request["SearchKey"];
            string settingsvalue = Request["SearchValue"];
            int totalCount = 0;
            SettingsParams settingsparams = new SettingsParams()
            {
                Key = settingskey,
                Value = settingsvalue,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            //var userinfo = userbll.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => true, c => c.Id, true);
            var settingsinfo = settingsbll.LoadSearchParams(settingsparams);
            var rows = from u in settingsinfo
                       select new { id = u.id, key = u.key, value = u.value };
            return Json(new { total = settingsparams.TotalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加数据
        public ActionResult AddSettingsInfo(Settings settingsadd)
        {
            Settings settingsinfo = new Settings();
            settingsinfo.key = settingsadd.key;
            settingsinfo.value = settingsadd.value;
            if (settingsbll.AddEntity(settingsinfo) != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 修改数据
        public ActionResult EditSettingsInfo(Settings settingsupdate)
        {
            int id = Convert.ToInt32(Request["Id"]);
            var settingsinfo = settingsbll.LoadEntities(c => c.id == id).FirstOrDefault();
            if (settingsinfo != null)
            {
                settingsinfo.key = settingsupdate.key;
                settingsinfo.value = settingsupdate.value;
                if (settingsbll.UpdateEntity(settingsinfo))
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
        #endregion

        #region 删除数据
        public ActionResult DeleteSettingsInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (settingsbll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #endregion

        #region 分类管理

        #region 页面展示
        public ActionResult ShowCategoriesInfo()
        {
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetCategoriesInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            string categoriesname = Request["SearchCategoriesName"];
            int totalCount = 0;
            CategoriesParams categoriesparams = new CategoriesParams()
            {
                CategoriesName = categoriesname,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            //var userinfo = userbll.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => true, c => c.Id, true);
            var categoriesinfo = categoriesbll.LoadSearchParams(categoriesparams);
            var rows = from u in categoriesinfo
                       select new { Id = u.Id, Name = u.Name, PId = u.PId };
            return Json(new { total = categoriesparams.TotalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加数据
        public ActionResult AddCategoriesInfo(Categories categoriesadd)
        {
            Categories categoriesinfo = new Categories();
            categoriesinfo.Name = categoriesadd.Name;
            categoriesinfo.PId = categoriesadd.PId;
            if (categoriesbll.AddEntity(categoriesinfo) != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 修改数据
        public ActionResult EditCategoriesInfo(Categories categoriesupdate)
        {
            int id = Convert.ToInt32(Request["Id"]);
            var categoriesinfo = categoriesbll.LoadEntities(c => c.Id == id).FirstOrDefault();
            if (categoriesinfo != null)
            {
                categoriesinfo.Name = categoriesupdate.Name;
                categoriesinfo.PId = categoriesupdate.PId;
                if (categoriesbll.UpdateEntity(categoriesinfo))
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
        #endregion

        #region 删除数据
        public ActionResult DeleteCategoriesInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (categoriesbll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #endregion

        #region 图书管理

        #region 页面展示
        public ActionResult ShowBooksInfo()
        {
            //查询所有出版社
            var publisherlist = publishbll.LoadEntities(c => true).ToList();
            ViewData["publisherlist"] = publisherlist;
            //查询所有分类
            var categorylist = categoriesbll.LoadEntities(c => true).ToList();
            ViewData["categorylist"] = categorylist;
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetBooksInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            string title = Request["SearchTitle"];
            string author = Request["SearchAuthor"];
            string publisher = Request["SearchPublisher"];
            string categories = Request["SearchCategories"];
            int totalCount = 0;
            BooksParams bookinfoparams = new BooksParams()
            {
                Title = title,
                Author = author,
                Publisher = publisher,
                Categories = categories,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            //var userinfo = userbll.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => true, c => c.Id, true);
            var bookinfo = booksbll.LoadSearchParams(bookinfoparams);
            var rows = from u in bookinfo
                       select new { Id = u.Id, Title = u.Title, Author = u.Author, Publisher = u.Publishers.Name, PublishDate = u.PublishDate, ISBN = u.ISBN, WordsCount = u.WordsCount, UnitPrice = u.UnitPrice, Category = u.Categories.Name, Discount = u.Discount };
            return Json(new { total = bookinfoparams.TotalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 查看图书详情
        public ActionResult BookDetails()
        {
            int id = Convert.ToInt32(Request["id"]);
            var booksmodel = booksbll.LoadEntities(c => c.Id == id).FirstOrDefault();
            if (booksmodel != null)
            {
                ViewData["booksmodel"] = booksmodel;
            }
            return View();
        }
        #endregion

        #region 添加展示
        public ActionResult AddBooksShow()
        {
            //查找出版社
            var publishmodel = publishbll.LoadEntities(c => true).ToList();
            if (publishmodel != null)
            {
                ViewData["publish"] = publishmodel;
            }
            //查找分类
            var catmodel = categoriesbll.LoadEntities(c => true).ToList();
            if (catmodel != null)
            {
                ViewData["catlist"] = catmodel;
            }
            return View();
        }
        #endregion

        #region 添加数据
        public void AddBooksInfo(Books book)
        {
            //上传图片
            HttpPostedFileBase upload = Request.Files["img"];
            string s = upload.FileName;
            string[] str = s.Split('.');
            if (str[1] != "jpg")
            {
                WebCommon.GoBack("对不起，图片只能上传jpg格式的！");
            }
            else
            {
                string path = "/Images/BookCovers/" + book.ISBN + ".jpg";
                var addbooks = booksbll.AddEntity(book);
                SearchIndexManager.GetInstance().AddQueue(book.Id, book.Title, book.ISBN, book.UnitPrice, book.ContentDescription, book.Discount);
                if (addbooks != null)
                {
                    upload.SaveAs(Server.MapPath(path));
                    WebCommon.ShowUrl("添加成功", "/Admin/AdminHome/ShowBooksInfo");
                }
                else
                {
                    WebCommon.GoBack("添加失败");
                }
            }

            //var list = booksbll.LoadEntities(c => true).ToList();

            //foreach (var book in list)
            //{
            //    SearchIndexManager.GetInstance().AddQueue(book.Id, book.Title, book.ISBN, book.UnitPrice, book.ContentDescription, book.Discount);
            //}

            //WebCommon.ShowUrl("添加成功111", "/Admin/AdminHome/ShowBooksInfo");
        }
        #endregion

        #region 修改展示
        public ActionResult EditBooksShow()
        {
            //展示数据
            int id = Convert.ToInt32(Request["id"]);
            var books = booksbll.LoadEntities(c => c.Id == id).FirstOrDefault();
            if (books != null)
            {
                ViewData["booksmodel"] = books;
            }
            //查找出版社
            var publishmodel = publishbll.LoadEntities(c => true).ToList();
            if (publishmodel != null)
            {
                ViewData["publish"] = publishmodel;
            }
            //查找分类
            var catmodel = categoriesbll.LoadEntities(c => true).ToList();
            if (catmodel != null)
            {
                ViewData["catlist"] = catmodel;
            }
            ViewData["imgisbn"] = books.ISBN;
            return View();
        }
        #endregion

        #region 修改数据
        [ValidateInput(false)]
        public void EditBooksInfo(Books books)
        {
            HttpPostedFileBase editupload = Request.Files["editimg"];
            string edit = editupload.FileName;
            if (!string.IsNullOrEmpty(edit))
            {
                string imgisbn = Request["imgisbn"];
                //删除原图片
                string path = Server.MapPath("/Images/BookCovers/" + imgisbn + ".jpg");
                System.IO.File.Delete(path);
                //更新图片              
                string[] editstr = edit.Split('.');
                if (editstr[1] != "jpg")
                {
                    WebCommon.GoBack("对不起，图片只能上传jpg格式的！");
                }
                else
                {
                    if (booksbll.UpdateEntity(books))
                    {
                        editupload.SaveAs(Server.MapPath("/Images/BookCovers/" + books.ISBN + ".jpg"));
                        WebCommon.ShowUrl("更新成功", "/Admin/AdminHome/ShowBooksInfo");
                    }
                    else
                    {
                        WebCommon.GoBack("更新失败");
                    }
                }
            }
            else
            {
                if (booksbll.UpdateEntity(books))
                {
                    WebCommon.ShowUrl("更新成功", "/Admin/AdminHome/ShowBooksInfo");
                }
                else
                {
                    WebCommon.GoBack("更新失败");
                }
            }

        }
        #endregion

        #region 删除数据
        public ActionResult DeleteBooksInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            //找到图片路径
            foreach (int item in list)
            {
                var bookid = booksbll.LoadEntities(c => c.Id == item).FirstOrDefault();
                string path = Server.MapPath("/Images/BookCovers/" + bookid.ISBN + ".jpg");
                System.IO.File.Delete(path);
            }
            if (booksbll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #endregion

        #region 出版社管理

        #region 页面展示
        public ActionResult ShowPublisherInfo()
        {
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetPublisherInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            string publishername = Request["SearchPublisherName"];
            int totalCount = 0;
            PublisherParams publisherparams = new PublisherParams()
            {
                PublisherName = publishername,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            //var userinfo = userbll.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => true, c => c.Id, true);
            var publisherinfo = publishbll.LoadSearchParams(publisherparams);
            var rows = from u in publisherinfo
                       select new { Id = u.Id, Name = u.Name };
            return Json(new { total = publisherparams.TotalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加数据
        public ActionResult AddPublisherInfo(Publishers publisheradd)
        {
            Publishers publisherinfo = new Publishers();
            publisherinfo.Name = publisheradd.Name;
            if (publishbll.AddEntity(publisherinfo) != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 修改数据
        public ActionResult EditPublisherInfo(Publishers publisherupdate)
        {
            int id = Convert.ToInt32(Request["Id"]);
            var publisherinfo = publishbll.LoadEntities(c => c.Id == id).FirstOrDefault();
            if (publisherinfo != null)
            {
                publisherinfo.Name = publisherupdate.Name;
                if (publishbll.UpdateEntity(publisherinfo))
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
        #endregion

        #region 删除数据
        public ActionResult DeletePublisherInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (publishbll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #endregion

        #region 订单管理

        #region 页面展示
        public ActionResult ShowOrderBookInfo()
        {
            //查询所有订单编号
            var orders = ordersbll.LoadEntities(c => true).ToList();
            ViewData["orders"] = orders;
            //查询所有图书
            var ordersbook = booksbll.LoadEntities(c => true).ToList();
            ViewData["books"] = ordersbook;
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetOrderBookInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            string orderid = Request["SearchOrderId"];
            int totalCount = 0;
            OrderBookParams orderbookparams = new OrderBookParams()
            {
                OrderId = orderid,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };
            var orderbookinfo = orderbookbll.LoadSearchParams(orderbookparams);
            var rows = from u in orderbookinfo
                       select new { Id = u.Id, OrderID = u.OrderID, BookName = u.Books.Title, Price = u.UnitPrice, Quantity = u.Quantity };
            return Json(new { total = orderbookparams.TotalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加数据
        public ActionResult AddOrderBookInfo(OrderBook orderbookadd)
        {
            OrderBook orderbookinfo = new OrderBook();
            orderbookinfo.OrderID = orderbookadd.OrderID;
            orderbookinfo.BookID = orderbookadd.BookID;
            orderbookinfo.UnitPrice = orderbookadd.UnitPrice;
            orderbookinfo.Quantity = orderbookadd.Quantity;
            if (orderbookbll.AddEntity(orderbookinfo) != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 修改数据
        public ActionResult EditOrderBookInfo(OrderBook orderbookupdate)
        {
            int id = Convert.ToInt32(Request["Id"]);
            var orderbookinfo = orderbookbll.LoadEntities(c => c.Id == id).FirstOrDefault();
            if (orderbookinfo != null)
            {
                orderbookinfo.OrderID = orderbookupdate.OrderID;
                orderbookinfo.BookID = orderbookupdate.BookID;
                orderbookinfo.UnitPrice = orderbookupdate.UnitPrice;
                orderbookinfo.Quantity = orderbookupdate.Quantity;
                if (orderbookbll.UpdateEntity(orderbookinfo))
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
        #endregion

        #region 删除数据
        public ActionResult DeleteOrderBookInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (orderbookbll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #endregion

        #region 订单详情管理
        #region 页面展示
        public ActionResult ShowOrdersInfo()
        {
            //查询所有用户
            var users = userbll.LoadEntities(c => true).ToList();
            ViewData["users"] = users;
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetOrdersInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            int totalCount = 0;
            var ordersinfo = ordersbll.LoadPageEntities<DateTime>(pageIndex, pageSize, out totalCount, c => true, c => c.OrderDate, false);
            var rows = from u in ordersinfo
                       select new { OrderId = u.OrderId, OrderDate = u.OrderDate, Mail = u.Users.Mail, TotalPrice = u.TotalPrice, PostAddress = u.PostAddress };
            return Json(new { total = totalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加数据
        public ActionResult AddOrdersInfo(Orders ordersadd)
        {
            Random random = new Random();
            int i = random.Next();
            Orders ordersinfo = new Orders();
            ordersinfo.OrderId = DateTime.Now.ToString("yyyyMMddhhmmss") + i.ToString().Substring(0, 5);
            ordersinfo.OrderDate = DateTime.Now;
            ordersinfo.UserId = ordersadd.UserId;
            ordersinfo.TotalPrice = ordersadd.TotalPrice;
            ordersinfo.PostAddress = ordersadd.PostAddress;
            if (ordersbll.AddEntity(ordersinfo) != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 修改数据
        public ActionResult EditOrdersInfo(Orders ordersupdate)
        {
            string id = Request["Id"];
            var ordersinfo = ordersbll.LoadEntities(c => c.OrderId == id).FirstOrDefault();
            if (ordersinfo != null)
            {
                ordersinfo.UserId = ordersupdate.UserId;
                ordersinfo.TotalPrice = ordersupdate.TotalPrice;
                ordersinfo.PostAddress = ordersupdate.PostAddress;
                if (ordersbll.UpdateEntity(ordersinfo))
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
        #endregion

        #region 删除数据
        public ActionResult DeleteOrdersInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<string> list = new List<string>();
            foreach (string id in strIds)
            {
                list.Add(id);
            }
            if (ordersbll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
        #endregion

        #region 购物车管理
        #region 页面展示
        public ActionResult ShowCartInfo()
        {
            //查询所有用户
            var users = userbll.LoadEntities(c => true).ToList();
            ViewData["users"] = users;
            //查询所有图书
            var ordersbook = booksbll.LoadEntities(c => true).ToList();
            ViewData["books"] = ordersbook;
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetCartInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            string cartmail = Request["SearchCartMail"];
            string carttitle = Request["SearchCartTitle"];
            int totalCount = 0;
            CartParams cartparams = new CartParams()
            {
                Mail = cartmail,
                Title = carttitle,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            //var userinfo = userbll.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => true, c => c.Id, true);
            var cartinfo = cartbll.LoadSearchParams(cartparams);
            var rows = from u in cartinfo
                       select new { Id = u.Id, Mail = u.Users.Mail, Title = u.Books.Title, Count = u.Count };
            return Json(new { total = cartparams.TotalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加数据
        public ActionResult AddCartInfo(Cart cartadd)
        {
            Cart cartinfo = new Cart();
            cartinfo.UserId = cartadd.UserId;
            cartinfo.BookId = cartadd.BookId;
            cartinfo.Count = cartadd.Count;
            if (cartbll.AddEntity(cartinfo) != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 修改数据
        public ActionResult EditCartInfo(Cart cartupdate)
        {
            int id = Convert.ToInt32(Request["Id"]);
            var cartinfo = cartbll.LoadEntities(c => c.Id == id).FirstOrDefault();
            if (cartinfo != null)
            {
                cartinfo.UserId = cartupdate.UserId;
                cartinfo.BookId = cartupdate.BookId;
                cartinfo.Count = cartupdate.Count;
                if (cartbll.UpdateEntity(cartinfo))
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
        #endregion

        #region 删除数据
        public ActionResult DeleteCartInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (cartbll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
        #endregion

        #region 图书评论管理
        #region 页面展示
        public ActionResult ShowCommentInfo()
        {
            //查询所有用户
            var users = userbll.LoadEntities(c => true).ToList();
            ViewData["users"] = users;
            //查询所有图书
            var ordersbook = booksbll.LoadEntities(c => true).ToList();
            ViewData["books"] = ordersbook;
            return View();
        }
        #endregion

        #region 传递数据
        public ActionResult GetCommentInfo()
        {
            int pageIndex;
            int pageSize;
            if (!int.TryParse(Request["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            if (!int.TryParse(Request["rows"], out pageSize))
            {
                pageSize = 5;
            }
            string commentmail = Request["SearchCommentMail"];
            string commenttitle = Request["SearchCommentTitle"];
            int totalCount = 0;
            CommentParams commentparams = new CommentParams()
            {
                Mail = commentmail,
                Title = commenttitle,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            //var userinfo = userbll.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => true, c => c.Id, true);
            var commentinfo = commentbll.LoadSearchParams(commentparams);
            var rows = from u in commentinfo
                       select new { Id = u.Id, Mail = u.Users.Mail, Title = u.Books.Title, Msg = u.Msg, RegTime = u.RegTime };
            return Json(new { total = commentparams.TotalCount, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加数据
        public ActionResult AddCommentInfo(BookComment commentadd)
        {
            BookComment commentinfo = new BookComment();
            commentinfo.UserId = commentadd.UserId;
            commentinfo.BookId = commentadd.BookId;
            commentinfo.Msg = commentadd.Msg;
            commentinfo.RegTime = DateTime.Now;
            if (commentbll.AddEntity(commentinfo) != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 修改数据
        public ActionResult EditCommentInfo(BookComment commentupdate)
        {
            int id = Convert.ToInt32(Request["Id"]);
            var commentinfo = commentbll.LoadEntities(c => c.Id == id).FirstOrDefault();
            if (commentinfo != null)
            {
                commentinfo.UserId = commentupdate.UserId;
                commentinfo.BookId = commentupdate.BookId;
                commentinfo.Msg = commentupdate.Msg;
                if (commentbll.UpdateEntity(commentinfo))
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
        #endregion

        #region 删除数据
        public ActionResult DeleteCommentInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (commentbll.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
        #endregion

        #region 敏感词
        public ActionResult ShowAddCode()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ShowAddCode(string txtCode)
        {
            string msg = Request["txtCode"];
            if (!string.IsNullOrEmpty(msg))
            {
                string[] words = msg.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    string[] w = word.Split('=');
                    ArticelWords article = new ArticelWords();
                    article.WordPattern = w[0];
                    if (w[1] == "{BANNED}")
                    {
                        article.IsForbid = true;
                    }
                    else if (w[1] == "{MOD}")
                    {
                        article.IsMod = true;
                    }
                    else
                    {
                        article.ReplaceWord = w[1];
                    }
                    abll.AddEntity(article);
                }
            }
            return Content("ok");
        }
        #endregion

        #region 注销
        public void Layout()
        {
            Session.Remove("admin");
            Response.Redirect("/Admin/AdminLogin/Index");
        }
        #endregion
    }
}
