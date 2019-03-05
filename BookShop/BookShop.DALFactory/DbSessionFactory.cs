using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace BookShop.DALFactory
{
    public class DbSessionFactory
    {
        /// <summary>
        /// 保证线程内唯一
        /// </summary>
        /// <returns></returns>
        public static DbSession CreateDbSession()
        {
            DbSession dbSession = (DbSession)CallContext.GetData("dbSession");
            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("dbSession", dbSession);
            }
            return dbSession;
        }
    }
}
