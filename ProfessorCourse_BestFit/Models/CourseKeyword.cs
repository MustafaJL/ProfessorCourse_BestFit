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
    
    public partial class CourseKeyword
    {
        public int course_keyword_Id { get; set; }
        public int Keyword_Id { get; set; }
        public int Course_Id { get; set; }
        public bool isDeleted { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Keyword Keyword { get; set; }
    }
}
