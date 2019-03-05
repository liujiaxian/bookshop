using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using BookShop.IBLL;
using BookShop.Common;

namespace BookShop.WebApp.Models
{
    public class IndexJobRatings : IJob
    {
        Irecommend_ratingBLL ratingbll = new BLL.recommend_ratingBLL();
        ISettingsBLL setingsbll = new BLL.SettingsBLL();
        #region IJob 成员
        /// <summary>
        /// 定时处理任务都要放在这个方法
        /// </summary>
        /// <param name="context"></param>
        public void Execute(JobExecutionContext context)
        {
            var list = ratingbll.LoadEntities(c => true).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                //需要过滤 取平均值
                sb.Append(item.userID + "\t" + item.bookID + "\t" + item.stars + "\t" + WebCommon.DateTimeToUnixTimestamp(Convert.ToDateTime(item.addTime)) + "\r\n");
            }
            var logmodel = setingsbll.LoadEntities(c=>c.id==16).FirstOrDefault();
            if (logmodel != null && logmodel.value == "true")
            {
                System.IO.File.WriteAllText(WebCommon.MapPath("/data/ratings.dat"), sb.ToString());//写入文件  
                logmodel.value = "false";
                setingsbll.UpdateEntity(logmodel);
            }
            else
            {
                System.IO.File.WriteAllText(WebCommon.MapPath("/data/ratings1.dat"), sb.ToString());//写入文件
                logmodel.value = "true";
                setingsbll.UpdateEntity(logmodel);
            }
        }

        #endregion
    }
}