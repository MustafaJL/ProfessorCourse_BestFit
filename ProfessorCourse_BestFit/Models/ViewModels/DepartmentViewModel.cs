using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public int Dep_Id { get; set; }

        public string Dep_Name { get; set; }

        public bool isDeleted { get; set; }

        public Nullable<int> User_Id { get; set; }


    }
}