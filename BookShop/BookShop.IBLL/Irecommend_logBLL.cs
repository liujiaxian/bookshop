using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Model;

namespace BookShop.IBLL
{
    public partial interface Irecommend_logBLL : IBaseBLL<recommend_log>
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
        void AddUserOperationLog(int userID,int bookID,string behaviorType,string context,string behaviorWeight,string behaviorContent);
    }
}
