using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.IDAL;
using BookShop.Model;
using System.Data.Entity;
using System.Data.Objects;
using BookShop.DAL;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookShop.DALFactory
{
    public partial class DbSession : IDbSession
    {
        //bookshopEntities db = new bookshopEntities();

        DbContext db = DbContextFactory.CreateDbContext();

        #region IDbSession 成员
        //private ICartDAL _cartDAL;
        //public ICartDAL CartDAL
        //{
        //    get
        //    {
        //        if (_cartDAL == null)
        //        {
        //            //return new DAL.CartDAL(); //应该需要再次解耦 使用抽象工厂 利用反射
        //            return AbstractFactory.CreateCartDAL();
        //        }
        //        return _cartDAL;
        //    }
        //    set
        //    {
        //        _cartDAL = value;
        //    }
        //}

        public int ExecuteSql(string sql, params SqlParameter[] pams)
        {
            return db.Database.ExecuteSqlCommand(sql, pams);
        }
        public List<string> ExcuteSelect(string sql, params SqlParameter[] pams)
        {
            return db.Database.SqlQuery<string>(sql, pams).ToList();
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        #endregion
    }
}
