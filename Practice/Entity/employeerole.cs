//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Practice.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class employeerole
    {
        public employeerole()
        {
            this.useraccount = new HashSet<useraccount>();
        }
    
        public int roleid { get; set; }
        public string rolename { get; set; }
        public string description { get; set; }
    
        public virtual ICollection<useraccount> useraccount { get; set; }
    }
}
