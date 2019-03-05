using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.WebApp.Models
{
    public class IndexJob:IJob
    {
        IBLL.IkeyWordsRankBLL bll = new BLL.keyWordsRankBLL();
        #region IJob 成员
        /// <summary>
        /// 定时处理任务都要放在这个方法
        /// </summary>
        /// <param name="context"></param>
        public void Execute(JobExecutionContext context)
        {
            bll.DeleteKeyWordsRank();//删除汇总表数据
            bll.InsertKeyWordsRank();//再向汇总表添加数据
        }

        #endregion
    }
}