//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProfessorCourse_BestFit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RolePermission
    {
        public int Id { get; set; }
        public int RId { get; set; }
        public int PId { get; set; }
        public bool isDeleted { get; set; }
    
        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
