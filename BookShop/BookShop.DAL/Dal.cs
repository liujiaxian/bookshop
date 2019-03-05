 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;
using BookShop.IDAL;

namespace BookShop.DAL
{
		
	public partial class ActionInfoDAL :BaseDAL<ActionInfo>,IActionInfoDAL
    {

    }
		
	public partial class ArticelWordsDAL :BaseDAL<ArticelWords>,IArticelWordsDAL
    {

    }
		
	public partial class BookCommentDAL :BaseDAL<BookComment>,IBookCommentDAL
    {

    }
		
	public partial class BooksDAL :BaseDAL<Books>,IBooksDAL
    {

    }
		
	public partial class CartDAL :BaseDAL<Cart>,ICartDAL
    {

    }
		
	public partial class CategoriesDAL :BaseDAL<Categories>,ICategoriesDAL
    {

    }
		
	public partial class ContactDAL :BaseDAL<Contact>,IContactDAL
    {

    }
		
	public partial class EmailActiveDAL :BaseDAL<EmailActive>,IEmailActiveDAL
    {

    }
		
	public partial class HotCountDAL :BaseDAL<HotCount>,IHotCountDAL
    {

    }
		
	public partial class keyWordsRankDAL :BaseDAL<keyWordsRank>,IkeyWordsRankDAL
    {

    }
		
	public partial class OrderBookDAL :BaseDAL<OrderBook>,IOrderBookDAL
    {

    }
		
	public partial class OrdersDAL :BaseDAL<Orders>,IOrdersDAL
    {

    }
		
	public partial class PayInfo_delDAL :BaseDAL<PayInfo_del>,IPayInfo_delDAL
    {

    }
		
	public partial class PdfReaderDAL :BaseDAL<PdfReader>,IPdfReaderDAL
    {

    }
		
	public partial class PublishersDAL :BaseDAL<Publishers>,IPublishersDAL
    {

    }
		
	public partial class recommend_logDAL :BaseDAL<recommend_log>,Irecommend_logDAL
    {

    }
		
	public partial class recommend_ratingDAL :BaseDAL<recommend_rating>,Irecommend_ratingDAL
    {

    }
		
	public partial class RoleInfoDAL :BaseDAL<RoleInfo>,IRoleInfoDAL
    {

    }
		
	public partial class SearchDatailsDAL :BaseDAL<SearchDatails>,ISearchDatailsDAL
    {

    }
		
	public partial class SettingsDAL :BaseDAL<Settings>,ISettingsDAL
    {

    }
		
	public partial class T_EmailDAL :BaseDAL<T_Email>,IT_EmailDAL
    {

    }
		
	public partial class UsersDAL :BaseDAL<Users>,IUsersDAL
    {

    }
		
	public partial class UserStatesDAL :BaseDAL<UserStates>,IUserStatesDAL
    {

    }
	
}