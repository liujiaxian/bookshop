 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.IDAL;
using BookShop.Model;
using System.Data.Entity;
using BookShop.DAL;

namespace BookShop.DALFactory
{
	public partial class DbSession : IDbSession
    {
	
		private IActionInfoDAL _ActionInfoDAL;
        public IActionInfoDAL ActionInfoDAL
        {
            get
            {
                if(_ActionInfoDAL == null)
                {
                    _ActionInfoDAL = AbstractFactory.CreateActionInfoDAL();

                }
                return _ActionInfoDAL;
            }
            set { _ActionInfoDAL = value; }
        }
	
		private IArticelWordsDAL _ArticelWordsDAL;
        public IArticelWordsDAL ArticelWordsDAL
        {
            get
            {
                if(_ArticelWordsDAL == null)
                {
                    _ArticelWordsDAL = AbstractFactory.CreateArticelWordsDAL();

                }
                return _ArticelWordsDAL;
            }
            set { _ArticelWordsDAL = value; }
        }
	
		private IBookCommentDAL _BookCommentDAL;
        public IBookCommentDAL BookCommentDAL
        {
            get
            {
                if(_BookCommentDAL == null)
                {
                    _BookCommentDAL = AbstractFactory.CreateBookCommentDAL();

                }
                return _BookCommentDAL;
            }
            set { _BookCommentDAL = value; }
        }
	
		private IBooksDAL _BooksDAL;
        public IBooksDAL BooksDAL
        {
            get
            {
                if(_BooksDAL == null)
                {
                    _BooksDAL = AbstractFactory.CreateBooksDAL();

                }
                return _BooksDAL;
            }
            set { _BooksDAL = value; }
        }
	
		private ICartDAL _CartDAL;
        public ICartDAL CartDAL
        {
            get
            {
                if(_CartDAL == null)
                {
                    _CartDAL = AbstractFactory.CreateCartDAL();

                }
                return _CartDAL;
            }
            set { _CartDAL = value; }
        }
	
		private ICategoriesDAL _CategoriesDAL;
        public ICategoriesDAL CategoriesDAL
        {
            get
            {
                if(_CategoriesDAL == null)
                {
                    _CategoriesDAL = AbstractFactory.CreateCategoriesDAL();

                }
                return _CategoriesDAL;
            }
            set { _CategoriesDAL = value; }
        }
	
		private IContactDAL _ContactDAL;
        public IContactDAL ContactDAL
        {
            get
            {
                if(_ContactDAL == null)
                {
                    _ContactDAL = AbstractFactory.CreateContactDAL();

                }
                return _ContactDAL;
            }
            set { _ContactDAL = value; }
        }
	
		private IEmailActiveDAL _EmailActiveDAL;
        public IEmailActiveDAL EmailActiveDAL
        {
            get
            {
                if(_EmailActiveDAL == null)
                {
                    _EmailActiveDAL = AbstractFactory.CreateEmailActiveDAL();

                }
                return _EmailActiveDAL;
            }
            set { _EmailActiveDAL = value; }
        }
	
		private IHotCountDAL _HotCountDAL;
        public IHotCountDAL HotCountDAL
        {
            get
            {
                if(_HotCountDAL == null)
                {
                    _HotCountDAL = AbstractFactory.CreateHotCountDAL();

                }
                return _HotCountDAL;
            }
            set { _HotCountDAL = value; }
        }
	
		private IkeyWordsRankDAL _keyWordsRankDAL;
        public IkeyWordsRankDAL keyWordsRankDAL
        {
            get
            {
                if(_keyWordsRankDAL == null)
                {
                    _keyWordsRankDAL = AbstractFactory.CreatekeyWordsRankDAL();

                }
                return _keyWordsRankDAL;
            }
            set { _keyWordsRankDAL = value; }
        }
	
		private IOrderBookDAL _OrderBookDAL;
        public IOrderBookDAL OrderBookDAL
        {
            get
            {
                if(_OrderBookDAL == null)
                {
                    _OrderBookDAL = AbstractFactory.CreateOrderBookDAL();

                }
                return _OrderBookDAL;
            }
            set { _OrderBookDAL = value; }
        }
	
		private IOrdersDAL _OrdersDAL;
        public IOrdersDAL OrdersDAL
        {
            get
            {
                if(_OrdersDAL == null)
                {
                    _OrdersDAL = AbstractFactory.CreateOrdersDAL();

                }
                return _OrdersDAL;
            }
            set { _OrdersDAL = value; }
        }
	
		private IPayInfo_delDAL _PayInfo_delDAL;
        public IPayInfo_delDAL PayInfo_delDAL
        {
            get
            {
                if(_PayInfo_delDAL == null)
                {
                    _PayInfo_delDAL = AbstractFactory.CreatePayInfo_delDAL();

                }
                return _PayInfo_delDAL;
            }
            set { _PayInfo_delDAL = value; }
        }
	
		private IPdfReaderDAL _PdfReaderDAL;
        public IPdfReaderDAL PdfReaderDAL
        {
            get
            {
                if(_PdfReaderDAL == null)
                {
                    _PdfReaderDAL = AbstractFactory.CreatePdfReaderDAL();

                }
                return _PdfReaderDAL;
            }
            set { _PdfReaderDAL = value; }
        }
	
		private IPublishersDAL _PublishersDAL;
        public IPublishersDAL PublishersDAL
        {
            get
            {
                if(_PublishersDAL == null)
                {
                    _PublishersDAL = AbstractFactory.CreatePublishersDAL();

                }
                return _PublishersDAL;
            }
            set { _PublishersDAL = value; }
        }
	
		private Irecommend_logDAL _recommend_logDAL;
        public Irecommend_logDAL recommend_logDAL
        {
            get
            {
                if(_recommend_logDAL == null)
                {
                    _recommend_logDAL = AbstractFactory.Createrecommend_logDAL();

                }
                return _recommend_logDAL;
            }
            set { _recommend_logDAL = value; }
        }
	
		private Irecommend_ratingDAL _recommend_ratingDAL;
        public Irecommend_ratingDAL recommend_ratingDAL
        {
            get
            {
                if(_recommend_ratingDAL == null)
                {
                    _recommend_ratingDAL = AbstractFactory.Createrecommend_ratingDAL();

                }
                return _recommend_ratingDAL;
            }
            set { _recommend_ratingDAL = value; }
        }
	
		private IRoleInfoDAL _RoleInfoDAL;
        public IRoleInfoDAL RoleInfoDAL
        {
            get
            {
                if(_RoleInfoDAL == null)
                {
                    _RoleInfoDAL = AbstractFactory.CreateRoleInfoDAL();

                }
                return _RoleInfoDAL;
            }
            set { _RoleInfoDAL = value; }
        }
	
		private ISearchDatailsDAL _SearchDatailsDAL;
        public ISearchDatailsDAL SearchDatailsDAL
        {
            get
            {
                if(_SearchDatailsDAL == null)
                {
                    _SearchDatailsDAL = AbstractFactory.CreateSearchDatailsDAL();

                }
                return _SearchDatailsDAL;
            }
            set { _SearchDatailsDAL = value; }
        }
	
		private ISettingsDAL _SettingsDAL;
        public ISettingsDAL SettingsDAL
        {
            get
            {
                if(_SettingsDAL == null)
                {
                    _SettingsDAL = AbstractFactory.CreateSettingsDAL();

                }
                return _SettingsDAL;
            }
            set { _SettingsDAL = value; }
        }
	
		private IT_EmailDAL _T_EmailDAL;
        public IT_EmailDAL T_EmailDAL
        {
            get
            {
                if(_T_EmailDAL == null)
                {
                    _T_EmailDAL = AbstractFactory.CreateT_EmailDAL();

                }
                return _T_EmailDAL;
            }
            set { _T_EmailDAL = value; }
        }
	
		private IUsersDAL _UsersDAL;
        public IUsersDAL UsersDAL
        {
            get
            {
                if(_UsersDAL == null)
                {
                    _UsersDAL = AbstractFactory.CreateUsersDAL();

                }
                return _UsersDAL;
            }
            set { _UsersDAL = value; }
        }
	
		private IUserStatesDAL _UserStatesDAL;
        public IUserStatesDAL UserStatesDAL
        {
            get
            {
                if(_UserStatesDAL == null)
                {
                    _UserStatesDAL = AbstractFactory.CreateUserStatesDAL();

                }
                return _UserStatesDAL;
            }
            set { _UserStatesDAL = value; }
        }
	}	
}