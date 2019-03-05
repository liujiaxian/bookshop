using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Model;
using BookShop.IBLL;
using System.Text.RegularExpressions;

namespace BookShop.BLL
{
    public partial class ArticelWordsBLL : IBaseBLL<ArticelWords>, IArticelWordsBLL
    {

        #region IArticelWordsBLL 成员
        /// <summary>
        /// 禁用词过滤
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool CheckBanndWord(string msg)
        {
            //从数据库中查询禁用词
            var list = this.DbSession.ArticelWordsDAL.LoadEntities(c => c.IsForbid == true).Select<ArticelWords, string>(c => c.WordPattern).ToList();
            string regex = string.Join("|", list.ToArray());
            return Regex.IsMatch(msg,regex);
        }
        /// <summary>
        /// 审查词过滤
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool CheckModWord(string msg)
        {
            //从数据库中查询审查词
            var list = this.DbSession.ArticelWordsDAL.LoadEntities(c => c.IsMod == true).Select<ArticelWords, string>(c => c.WordPattern).ToList();
            //正则匹配
            string regex = string.Join("|",list.ToArray());
            regex = regex.Replace(@"\",@"\\").Replace(@"{2}",@".{0,2}");
            return Regex.IsMatch(msg,regex);
        }

        #endregion
    }
}
