//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManager.Infrastructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class ref_Status
    {
        public ref_Status()
        {
            this.Tasks = new HashSet<Task>();
        }
    
        public int StatusId { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<Task> Tasks { get; set; }
    }
}