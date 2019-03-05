using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.IBLL
{
    public interface IBaseBLL<T> where T:class,new()
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <param name="isAsc">升序或降序</param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderLambda, bool isAsc);
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc);
        //添加
        T AddEntity(T entity);
        //修改
        bool UpdateEntity(T entity);
        //删除
        bool DeleteEntity(T entity);
    }
}
