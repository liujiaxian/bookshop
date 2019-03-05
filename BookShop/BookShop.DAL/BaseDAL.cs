using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace BookShop.DAL
{
    public class BaseDAL<T> where T:class,new()
    {
        //bookshopEntities db = new bookshopEntities();
        DbContext db = DbContextFactory.CreateDbContext();
        #region IBaseDAL<T> 成员
        //查询
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where<T>(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderLambda, bool isAsc)
        {
            var cart = db.Set<T>().Where(whereLambda);
            totalCount = cart.Count();//查询数据总数
            if (isAsc)//为true表示升序
            {
                cart = cart.OrderBy<T, s>(orderLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                cart = cart.OrderByDescending<T, s>(orderLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return cart;
        }
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = db.Set<T>().Where<T>(whereLambda);
            if (isAsc)//表示升序
            {
                temp = temp.OrderBy<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }
        public T AddEntity(T entity)
        {
            db.Set<T>().Add(entity);
            //db.SaveChanges();
            return entity;
        }

        public bool UpdateEntity(T entity)
        {
            db.Entry<T>(entity).State = System.Data.EntityState.Modified;
            return true;
        }

        public bool DeleteEntity(T entity)
        {
            db.Entry<T>(entity).State = System.Data.EntityState.Deleted;
            return true;
        }

        #endregion
    }
}
