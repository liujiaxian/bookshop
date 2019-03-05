using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Model;
using BookShop.IBLL;
using System.Data.SqlClient;

namespace BookShop.BLL
{
    public partial class keyWordsRankBLL:BaseBLL<keyWordsRank>,IkeyWordsRankBLL
    {
        #region IkeyWordsRankBLL 成员
        /// <summary>
        /// 首先删除汇总表数据
        /// </summary>
        public void DeleteKeyWordsRank()
        {
            this.DbSession.ExecuteSql("truncate table keyWordsRank");
        }

        #endregion

        #region IkeyWordsRankBLL 成员

        /// <summary>
        /// 向汇总表添加数据
        /// </summary>
        public void InsertKeyWordsRank()
        {
            string sql = "insert into keyWordsRank(Id,KeyWords,SearchTimes) select newid(),KeyWords,count(*) from SearchDatails where DateDiff(day,SearchDateTime,getdate())<=7 group by KeyWords";
            this.DbSession.ExecuteSql(sql);
        }

        #endregion

        #region IkeyWordsRankBLL 成员


        public List<string> GetSearchWord(string msg)
        {
            string sql = "select top 10 KeyWords from keyWordsRank where KeyWords like @term order by SearchTimes desc";
            return this.DbSession.ExcuteSelect(sql, new SqlParameter("@term", msg + "%"));
        }

        #endregion
    }
}
