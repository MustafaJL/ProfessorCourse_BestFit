﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        public List<UserRolesViewModel> list_Professors { get; set; }

        public List<ProgramViewModel> list_programs { get; set; }




    }
}