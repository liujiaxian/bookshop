﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookShop.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class bookshopEntities : DbContext
    {
        public bookshopEntities()
            : base("name=bookshopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ActionInfo> ActionInfo { get; set; }
        public DbSet<ArticelWords> ArticelWords { get; set; }
        public DbSet<BookComment> BookComment { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<EmailActive> EmailActive { get; set; }
        public DbSet<HotCount> HotCount { get; set; }
        public DbSet<keyWordsRank> keyWordsRank { get; set; }
        public DbSet<OrderBook> OrderBook { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<PayInfo_del> PayInfo_del { get; set; }
        public DbSet<PdfReader> PdfReader { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<recommend_log> recommend_log { get; set; }
        public DbSet<recommend_rating> recommend_rating { get; set; }
        public DbSet<RoleInfo> RoleInfo { get; set; }
        public DbSet<SearchDatails> SearchDatails { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<T_Email> T_Email { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserStates> UserStates { get; set; }
    
        public virtual int CreateOrderTest(string orderId, Nullable<int> userId, string address, ObjectParameter totalPrice)
        {
            var orderIdParameter = orderId != null ?
                new ObjectParameter("OrderId", orderId) :
                new ObjectParameter("OrderId", typeof(string));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateOrderTest", orderIdParameter, userIdParameter, addressParameter, totalPrice);
        }
    
        public virtual int Pro_CreateOrder(Nullable<int> userId, string orderNumber, string postAddress, ObjectParameter totalPrice)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var orderNumberParameter = orderNumber != null ?
                new ObjectParameter("OrderNumber", orderNumber) :
                new ObjectParameter("OrderNumber", typeof(string));
    
            var postAddressParameter = postAddress != null ?
                new ObjectParameter("PostAddress", postAddress) :
                new ObjectParameter("PostAddress", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pro_CreateOrder", userIdParameter, orderNumberParameter, postAddressParameter, totalPrice);
        }
    
        public virtual int Pro_GetPagedList(Nullable<int> start, Nullable<int> end, Nullable<int> category, string order)
        {
            var startParameter = start.HasValue ?
                new ObjectParameter("start", start) :
                new ObjectParameter("start", typeof(int));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("end", end) :
                new ObjectParameter("end", typeof(int));
    
            var categoryParameter = category.HasValue ?
                new ObjectParameter("category", category) :
                new ObjectParameter("category", typeof(int));
    
            var orderParameter = order != null ?
                new ObjectParameter("order", order) :
                new ObjectParameter("order", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pro_GetPagedList", startParameter, endParameter, categoryParameter, orderParameter);
        }
    
        public virtual int Pro_OrderCreate(string orderNmber, Nullable<int> userId, string address, ObjectParameter totalMoney)
        {
            var orderNmberParameter = orderNmber != null ?
                new ObjectParameter("OrderNmber", orderNmber) :
                new ObjectParameter("OrderNmber", typeof(string));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pro_OrderCreate", orderNmberParameter, userIdParameter, addressParameter, totalMoney);
        }
    
        public virtual int Pro_Page(string tblName, string strGetFields, string fldName, Nullable<int> pageSize, Nullable<int> pageIndex, Nullable<bool> doCount, Nullable<bool> orderType, string strWhere)
        {
            var tblNameParameter = tblName != null ?
                new ObjectParameter("tblName", tblName) :
                new ObjectParameter("tblName", typeof(string));
    
            var strGetFieldsParameter = strGetFields != null ?
                new ObjectParameter("strGetFields", strGetFields) :
                new ObjectParameter("strGetFields", typeof(string));
    
            var fldNameParameter = fldName != null ?
                new ObjectParameter("fldName", fldName) :
                new ObjectParameter("fldName", typeof(string));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var doCountParameter = doCount.HasValue ?
                new ObjectParameter("doCount", doCount) :
                new ObjectParameter("doCount", typeof(bool));
    
            var orderTypeParameter = orderType.HasValue ?
                new ObjectParameter("OrderType", orderType) :
                new ObjectParameter("OrderType", typeof(bool));
    
            var strWhereParameter = strWhere != null ?
                new ObjectParameter("strWhere", strWhere) :
                new ObjectParameter("strWhere", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Pro_Page", tblNameParameter, strGetFieldsParameter, fldNameParameter, pageSizeParameter, pageIndexParameter, doCountParameter, orderTypeParameter, strWhereParameter);
        }
    
        public virtual int CreateOrder(string orderId, Nullable<int> userId, string address, ObjectParameter totalPrice)
        {
            var orderIdParameter = orderId != null ?
                new ObjectParameter("OrderId", orderId) :
                new ObjectParameter("OrderId", typeof(string));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateOrder", orderIdParameter, userIdParameter, addressParameter, totalPrice);
        }
    }
}
