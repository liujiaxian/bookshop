using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace BookShop.DAL
{
    public class DbContextFactory
    {
        /// <summary>
        /// 保证线程内唯一
        /// </summary>
        /// <returns></returns>
        public static DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("dbContext");
            if (dbContext == null)
            {
                dbContext = new bookshopEntities();
                CallContext.SetData("dbContext",dbContext);
            }
            return dbContext;
        }
    }
}
