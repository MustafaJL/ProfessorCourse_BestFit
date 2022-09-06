using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public int Dep_Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Department Name")]
        public string Dep_Name { get; set; }

        public bool isDeleted { get; set; }

        public string User_id { get; set; }

        public int employee_count { get; set; }

        [Display(Name = "Manager User Name")]
        public string Manager_User_Name { get; set; }

        //this list for chose the managers of the department
        public IEnumerable<UserRolesViewModel> List_User_Details { get; set; }

        public IEnumerable<Department> all_departments { get; set; }

        public Department Department { get; set; }
        
        public IEnumerable<User> List_Managers_Details { get; set; }
        
        public IEnumerable<User> all_users { get; set; }

        public IEnumerable<Program> List_All_Programs { get; set; }
    }
}