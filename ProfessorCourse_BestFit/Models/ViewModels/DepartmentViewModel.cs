using System;
using System.Collections.Generic;
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

        public Nullable<int> User_Id { get; set; }

        [Display(Name = "Manager User Name")]
        [Required(ErrorMessage = "Select Name before submit")]
        public string Manager_User_Name { get; set; }

        public List<UserRolesViewModel> List_User_Details { get; set; }

        public List<MultiSelectList> multiselectListmanager { get; set; }

        public List<SelectListItem> all_professors { get; set; }

        public List<DepartmentViewModel> departmentViewModels { get; set; }

        public string tree_id { get; set; }
        public string tree_text { get; set; }


    }
}