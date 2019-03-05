using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.IDAL;

namespace BookShop.BLL
{
    public abstract class BaseBLL<T> where T:class,new()
    {
        //利用工厂解耦
        public IDbSession DbSession
        {
            get 
            {
                //return new DALFactory.DbSession();//需要优化
                return DALFactory.DbSessionFactory.CreateDbSession();
            }
        }
        //创建当前DAL的方法
        public IBaseDAL<T> CurrentDAL { get; set; }
        //利用抽象方法来确定当前的DAL
        public abstract void SetCurrentDAL();

        public BaseBLL()
        {
            SetCurrentDAL();//子类必须执行该方法
        }

        #region IBaseBLL<T> 成员

        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDAL.LoadEntities(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderLambda, bool isAsc)
        {
            return CurrentDAL.LoadPageEntities(pageIndex,pageSize,out totalCount,whereLambda,orderLambda,isAsc);
        }
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            return CurrentDAL.LoadPageEntities(pageIndex, pageSize, whereLambda, orderbyLambda, isAsc);
        }
        public T AddEntity(T entity)
        {
            CurrentDAL.AddEntity(entity);
            DbSession.SaveChanges();
            return entity;
        }

        public bool UpdateEntity(T entity)
        {
            CurrentDAL.UpdateEntity(entity);
            return DbSession.SaveChanges() > 0;
        }

        public bool DeleteEntity(T entity)
        {
            CurrentDAL.DeleteEntity(entity);
            return DbSession.SaveChanges() > 0;
        }

        #endregion
    }
}
