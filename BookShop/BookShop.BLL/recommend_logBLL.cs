using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.IBLL;
using BookShop.Model;

namespace BookShop.BLL
{
    public partial class recommend_logBLL : BaseBLL<recommend_log>, Irecommend_logBLL
    {
        /// <summary>
        /// 添加用户行为日志
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="bookID">图书ID</param>
        /// <param name="behaviorType">行为种类（购买还是浏览）</param>
        /// <param name="context">产生行为的上下文（时间和地点）</param>
        /// <param name="behaviorWeight">行为的权重（打分行为，分数）</param>
        /// <param name="behaviorContent">行为的内容（如果是评论，评论文本）</param>
        public void AddUserOperationLog(int userID, int bookID,string behaviorType="",string context="",string behaviorWeight="",string behaviorContent="")
        {
            var model = DbSession.recommend_logDAL.LoadEntities(c => c.userID == userID && c.itemID == bookID).FirstOrDefault();
            if (model==null)
            {
                recommend_log log = new recommend_log();
                log.userID = userID;
                log.itemID = bookID;
                log.behaviorType = behaviorType;
                log.context = context;
                log.behaviorWeight = behaviorWeight;
                log.behaviorContent = behaviorContent;
                log.addTime = DateTime.Now;
                DbSession.recommend_logDAL.AddEntity(log);
                DbSession.SaveChanges();
            }
        }
    }
}
