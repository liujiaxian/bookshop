 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BookShop.IDAL
{
	public partial interface IDbSession
    {

	
		IActionInfoDAL ActionInfoDAL{get;set;}
	
		IArticelWordsDAL ArticelWordsDAL{get;set;}
	
		IBookCommentDAL BookCommentDAL{get;set;}
	
		IBooksDAL BooksDAL{get;set;}
	
		ICartDAL CartDAL{get;set;}
	
		ICategoriesDAL CategoriesDAL{get;set;}
	
		IContactDAL ContactDAL{get;set;}
	
		IEmailActiveDAL EmailActiveDAL{get;set;}
	
		IHotCountDAL HotCountDAL{get;set;}
	
		IkeyWordsRankDAL keyWordsRankDAL{get;set;}
	
		IOrderBookDAL OrderBookDAL{get;set;}
	
		IOrdersDAL OrdersDAL{get;set;}
	
		IPayInfo_delDAL PayInfo_delDAL{get;set;}
	
		IPdfReaderDAL PdfReaderDAL{get;set;}
	
		IPublishersDAL PublishersDAL{get;set;}
	
		Irecommend_logDAL recommend_logDAL{get;set;}
	
		Irecommend_ratingDAL recommend_ratingDAL{get;set;}
	
		IRoleInfoDAL RoleInfoDAL{get;set;}
	
		ISearchDatailsDAL SearchDatailsDAL{get;set;}
	
		ISettingsDAL SettingsDAL{get;set;}
	
		IT_EmailDAL T_EmailDAL{get;set;}
	
		IUsersDAL UsersDAL{get;set;}
	
		IUserStatesDAL UserStatesDAL{get;set;}
	}	
}