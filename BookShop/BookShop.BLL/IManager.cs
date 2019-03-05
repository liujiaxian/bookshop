 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;
using BookShop.IBLL;

namespace BookShop.BLL
{
	
	public partial class ActionInfoBLL :BaseBLL<ActionInfo>,IActionInfoBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.ActionInfoDAL;
        }
    }   
	
	public partial class ArticelWordsBLL :BaseBLL<ArticelWords>,IArticelWordsBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.ArticelWordsDAL;
        }
    }   
	
	public partial class BookCommentBLL :BaseBLL<BookComment>,IBookCommentBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.BookCommentDAL;
        }
    }   
	
	public partial class BooksBLL :BaseBLL<Books>,IBooksBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.BooksDAL;
        }
    }   
	
	public partial class CartBLL :BaseBLL<Cart>,ICartBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.CartDAL;
        }
    }   
	
	public partial class CategoriesBLL :BaseBLL<Categories>,ICategoriesBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.CategoriesDAL;
        }
    }   
	
	public partial class ContactBLL :BaseBLL<Contact>,IContactBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.ContactDAL;
        }
    }   
	
	public partial class EmailActiveBLL :BaseBLL<EmailActive>,IEmailActiveBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.EmailActiveDAL;
        }
    }   
	
	public partial class HotCountBLL :BaseBLL<HotCount>,IHotCountBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.HotCountDAL;
        }
    }   
	
	public partial class keyWordsRankBLL :BaseBLL<keyWordsRank>,IkeyWordsRankBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.keyWordsRankDAL;
        }
    }   
	
	public partial class OrderBookBLL :BaseBLL<OrderBook>,IOrderBookBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.OrderBookDAL;
        }
    }   
	
	public partial class OrdersBLL :BaseBLL<Orders>,IOrdersBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.OrdersDAL;
        }
    }   
	
	public partial class PayInfo_delBLL :BaseBLL<PayInfo_del>,IPayInfo_delBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.PayInfo_delDAL;
        }
    }   
	
	public partial class PdfReaderBLL :BaseBLL<PdfReader>,IPdfReaderBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.PdfReaderDAL;
        }
    }   
	
	public partial class PublishersBLL :BaseBLL<Publishers>,IPublishersBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.PublishersDAL;
        }
    }   
	
	public partial class recommend_logBLL :BaseBLL<recommend_log>,Irecommend_logBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.recommend_logDAL;
        }
    }   
	
	public partial class recommend_ratingBLL :BaseBLL<recommend_rating>,Irecommend_ratingBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.recommend_ratingDAL;
        }
    }   
	
	public partial class RoleInfoBLL :BaseBLL<RoleInfo>,IRoleInfoBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.RoleInfoDAL;
        }
    }   
	
	public partial class SearchDatailsBLL :BaseBLL<SearchDatails>,ISearchDatailsBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.SearchDatailsDAL;
        }
    }   
	
	public partial class SettingsBLL :BaseBLL<Settings>,ISettingsBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.SettingsDAL;
        }
    }   
	
	public partial class T_EmailBLL :BaseBLL<T_Email>,IT_EmailBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.T_EmailDAL;
        }
    }   
	
	public partial class UsersBLL :BaseBLL<Users>,IUsersBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.UsersDAL;
        }
    }   
	
	public partial class UserStatesBLL :BaseBLL<UserStates>,IUserStatesBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.DbSession.UserStatesDAL;
        }
    }   
	
}