//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Instagram.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DisLike
    {
        public int dislikeId { get; set; }
        public Nullable<int> aers { get; set; }
        public Nullable<int> accountId { get; set; }
        public Nullable<int> postId { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Post Post { get; set; }
    }
}
