using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BookShop.IDAL
{
    public partial interface IDbSession
    {
        //创建对象
        //ICartDAL CartDAL { get; set; }
        //创建执行的特殊的SQL语句
        int ExecuteSql(string sql,params SqlParameter[] pams);
        List<string> ExcuteSelect(string sql, params SqlParameter[] pams);
        //创建一次性保存到数据库的方法
        int SaveChanges();
    }
}
