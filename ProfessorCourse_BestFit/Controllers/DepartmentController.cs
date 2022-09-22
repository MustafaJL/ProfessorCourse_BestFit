using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProfessorCourse_BestFit.Messages;
using Newtonsoft.Json;

namespace ProfessorCourse_BestFit.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities1 _context;
        private readonly Department_DAL department_DAL;
        private readonly User_DAL user_DAL;
        //private readonly Messages messages;
        public DepartmentController()
        {
            _context = new ProfessorCourseBestFit1Entities1();
            department_DAL = new Department_DAL();
            user_DAL = new User_DAL();
            //messages = new Messages();
        }

        private bool Name_Required(string name)
        {
            if (name == null)
            {
                return true;
            }
            return false;
        }

        private bool Name_Exist(string name)
        {
            var check_Name = _context.Departments.Where(
                x => x.Dep_Name == name
                ).FirstOrDefault();

            if (check_Name != null)
            {
                return true;
            }
            return false;
        }

        //GET :
        public ActionResult All_Departments()
        {
            var departmentViewModel = new DepartmentViewModel();

            departmentViewModel.all_Departments = _context.Departments.ToList();

            return View(departmentViewModel);
        }

        //GET :
        public ActionResult Create_Department()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Department(DepartmentViewModel departmentViewModel)
        {
            if (Name_Required(departmentViewModel.Department.Dep_Name))
            {
               ViewBag.nameRequired = "Please you should write name first.";
                ViewBag.data_not_saved = "The data is not saved.";
                return View(departmentViewModel);
            }

            if (Name_Exist(departmentViewModel.Department.Dep_Name))
            {
                ViewBag.nameExist = "The name you entered is already exist please chose another name.";
                ViewBag.data_not_saved = "The data is not saved."; 
                return View(departmentViewModel);
            }

            _context.Departments.Add(departmentViewModel.Department);
            _context.SaveChanges();

            ViewBag.Done = "Done...";
            ViewBag.Saved = "The data has been saved successfully.";

            return View(departmentViewModel);
        }

        //GET :
        public ActionResult view_Department_Information(int id)
        {
            var departmentViewModel = new DepartmentViewModel();

            departmentViewModel.Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            departmentViewModel.managers = user_DAL.Get_Users_Department(id, 1);

            departmentViewModel.employee = user_DAL.Get_Users_Department(id, 2);

            return View(departmentViewModel);
        }

        //GET :
        public ActionResult Edit_Department_Name(int id)
        {
            var departmentViewModel = new DepartmentViewModel();

            departmentViewModel.Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            return View(departmentViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Department_Name(DepartmentViewModel departmentViewModel, int id)
        {
            if (Name_Required(departmentViewModel.Department.Dep_Name))
            {
                ViewBag.nameRequired = "Please you should write name first.";
                ViewBag.data_not_saved = "The data is not saved.";
                return View(departmentViewModel);
            }

            if (Name_Exist(departmentViewModel.Department.Dep_Name))
            {
                ViewBag.nameExist = "The name you entered is already exist please chose another name.";
                ViewBag.data_not_saved = "The data is not saved.";
                return View(departmentViewModel);
            }

            var edit_Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            edit_Department.Dep_Name = departmentViewModel.Department.Dep_Name;
            _context.SaveChanges();

            ViewBag.Done = "Done...";
            ViewBag.Saved = "The data has been saved successfully.";

            return View(departmentViewModel);
        }

        [HttpPost]
        public JsonResult Deactivate_Department(int id)
        {
            var delete_Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            delete_Department.isDeleted = true;
            _context.SaveChanges();

            return Json(new
            {
                redirectUrl = Url.Action("All_Departments", "Department"),
                isRedirect = true
            });
        }

        [HttpPost]
        public JsonResult Activate_Department(int id)
        {
            var delete_Department = _context.Departments.Where(
                x => x.DepId == id
                ).FirstOrDefault();

            delete_Department.isDeleted = false;
            _context.SaveChanges();

            return Json(new
            {
                redirectUrl = Url.Action("All_Departments", "Department"),
                isRedirect = true
            });
        }

        //GET :
        public ActionResult Add_Remove_Department_Managers(int id)
        {
            var departmentViewModel = new DepartmentViewModel();
            departmentViewModel.Department = _context.Departments.Where(x => x.DepId == id).FirstOrDefault();
            departmentViewModel.normal_Users = user_DAL.Get_Users_Department(id, 3);
            departmentViewModel.managers = user_DAL.Get_Users_Department(id, 1);
            return View(departmentViewModel);
        }

        [HttpPost]
        public JsonResult Add_Remove_Department_Managers(int id, string[] ids)
        {
            var data = ids;
            if (data == null || data.Length == 0)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("All_Departments", "Department"),
                    isRedirect = true
                });
            }

            if (data[0] == "null data")
            {
                _context.DeleteAll(id, "department", "managers");
                _context.SaveChanges();

                return Json(new
                {
                    redirectUrl = Url.Action("All_Departments", "Department"),
                    isRedirect = true
                });
            }


            var s = "";
            for (int i = 0; i < data.Length; i++)
            {
                s += id + "," + data[i] + "," + DateTime.Now + "," + true + "]";
            }

            _context.AddRemove(id, s, "departmentmanagers", "]", ",");
            _context.SaveChanges();

            return Json(new
            {
                redirectUrl = Url.Action("All_Departments", "Department"),
                isRedirect = true
            });
        }

        //GET :
        public ActionResult Add_Remove_Department_Employees(int id)
        {
            var departmentViewModel = new DepartmentViewModel();
            departmentViewModel.Department = _context.Departments.Where(x => x.DepId == id).FirstOrDefault();
            departmentViewModel.normal_Users = user_DAL.Get_Users_Department(id, 4);
            departmentViewModel.employee = user_DAL.Get_Users_Department(id, 2);
            return View(departmentViewModel);
        }

        [HttpPost]
        public JsonResult Add_Remove_Department_Employees(int id, string[] ids)
        {
            var data = ids;
            if (data == null || data.Length == 0)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("All_Departments", "Department"),
                    isRedirect = true
                });
            }

            if (data[0] == "null data")
            {
                _context.DeleteAll(id, "department", "employees");
                _context.SaveChanges();

                return Json(new
                {
                    redirectUrl = Url.Action("All_Departments", "Department"),
                    isRedirect = true
                });
            }

            var s = "";
            for (int i = 0; i < data.Length; i++)
            {
                s+= id+","+data[i]+","+DateTime.Now+","+false+"]";
            }

            _context.AddRemove(id, s, "departmentemployees", "]", ",");
            _context.SaveChanges();


            return Json(new
            {
                redirectUrl = Url.Action("All_Departments", "Department"),
                isRedirect = true
            });
        }

        //GET :
        public ActionResult Add_Remove_Department_Programs(int id)
        {
            var departmentViewModel = new DepartmentViewModel();
            departmentViewModel.Department = _context.Departments.Where(x => x.DepId == id).FirstOrDefault();
            departmentViewModel.department_Programs = _context.Programs.Where(y => y.Dep_Id == id && y.isDeleted == false).ToList();
            departmentViewModel.all_Programs = _context.Programs.Where(z => z.Dep_Id == null && z.isDeleted == false).ToList();
            return View(departmentViewModel);
        }

        [HttpPost]
        public JsonResult Add_Remove_Department_Programs(int id, string[] ids)
        {
            var data = ids;
            if (data == null || data.Length == 0)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("All_Departments", "Department"),
                    isRedirect = true
                });
            }

            if (data[0] == "null data")
            {
                _context.DeleteAll(id, "department", "programs");
                _context.SaveChanges();

                return Json(new
                {
                    redirectUrl = Url.Action("All_Departments", "Department"),
                    isRedirect = true
                });
            }

            var s = "";
            for (int i = 0; i < data.Length; i++)
            {
                s += id + "," + data[i] + "]";
            }

            _context.AddRemove(id, s, "departmentprograms", "]", ",");
            _context.SaveChanges();


            return Json(new
            {
                redirectUrl = Url.Action("All_Departments", "Department"),
                isRedirect = true
            });
        }
    }
}