//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Users
    {
        public Users()
        {
            this.BookComment = new HashSet<BookComment>();
            this.Cart = new HashSet<Cart>();
            this.EmailActive = new HashSet<EmailActive>();
            this.Orders = new HashSet<Orders>();
            this.PayInfo_del = new HashSet<PayInfo_del>();
            this.PdfReader = new HashSet<PdfReader>();
            this.recommend_rating = new HashSet<recommend_rating>();
        }
    
        public int Id { get; set; }
        public string LoginId { get; set; }
        public string LoginPwd { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public int UserRoleId { get; set; }
        public int UserStateId { get; set; }
        public decimal Money { get; set; }
        public Nullable<bool> UserActive { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> RegTime { get; set; }
    
        public virtual ICollection<BookComment> BookComment { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<EmailActive> EmailActive { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<PayInfo_del> PayInfo_del { get; set; }
        public virtual ICollection<PdfReader> PdfReader { get; set; }
        public virtual ICollection<recommend_rating> recommend_rating { get; set; }
        public virtual RoleInfo RoleInfo { get; set; }
        public virtual UserStates UserStates { get; set; }
    }
}
