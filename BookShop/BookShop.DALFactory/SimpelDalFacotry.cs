 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using BookShop.IDAL;
using System.Reflection;

namespace BookShop.DALFactory
{
    public partial class AbstractFactory
    {
      
   
		      
	    public static IActionInfoDAL CreateActionInfoDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".ActionInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IActionInfoDAL;
        }
		      
	    public static IArticelWordsDAL CreateArticelWordsDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".ArticelWordsDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IArticelWordsDAL;
        }
		      
	    public static IBookCommentDAL CreateBookCommentDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".BookCommentDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IBookCommentDAL;
        }
		      
	    public static IBooksDAL CreateBooksDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".BooksDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IBooksDAL;
        }
		      
	    public static ICartDAL CreateCartDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".CartDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as ICartDAL;
        }
		      
	    public static ICategoriesDAL CreateCategoriesDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".CategoriesDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as ICategoriesDAL;
        }
		      
	    public static IContactDAL CreateContactDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".ContactDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IContactDAL;
        }
		      
	    public static IEmailActiveDAL CreateEmailActiveDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".EmailActiveDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IEmailActiveDAL;
        }
		      
	    public static IHotCountDAL CreateHotCountDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".HotCountDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IHotCountDAL;
        }
		      
	    public static IkeyWordsRankDAL CreatekeyWordsRankDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".keyWordsRankDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IkeyWordsRankDAL;
        }
		      
	    public static IOrderBookDAL CreateOrderBookDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".OrderBookDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IOrderBookDAL;
        }
		      
	    public static IOrdersDAL CreateOrdersDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".OrdersDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IOrdersDAL;
        }
		      
	    public static IPayInfo_delDAL CreatePayInfo_delDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".PayInfo_delDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IPayInfo_delDAL;
        }
		      
	    public static IPdfReaderDAL CreatePdfReaderDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".PdfReaderDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IPdfReaderDAL;
        }
		      
	    public static IPublishersDAL CreatePublishersDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".PublishersDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IPublishersDAL;
        }
		      
	    public static Irecommend_logDAL Createrecommend_logDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".recommend_logDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as Irecommend_logDAL;
        }
		      
	    public static Irecommend_ratingDAL Createrecommend_ratingDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".recommend_ratingDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as Irecommend_ratingDAL;
        }
		      
	    public static IRoleInfoDAL CreateRoleInfoDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".RoleInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IRoleInfoDAL;
        }
		      
	    public static ISearchDatailsDAL CreateSearchDatailsDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".SearchDatailsDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as ISearchDatailsDAL;
        }
		      
	    public static ISettingsDAL CreateSettingsDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".SettingsDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as ISettingsDAL;
        }
		      
	    public static IT_EmailDAL CreateT_EmailDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".T_EmailDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IT_EmailDAL;
        }
		      
	    public static IUsersDAL CreateUsersDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".UsersDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IUsersDAL;
        }
		      
	    public static IUserStatesDAL CreateUserStatesDAL()
        {
			string AssemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
			string NameSpace = ConfigurationManager.AppSettings["NameSpace"];
            string fullassemblyPath = NameSpace + ".UserStatesDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(fullassemblyPath,AssemblyPath);


            return obj as IUserStatesDAL;
        }
	}
	
}