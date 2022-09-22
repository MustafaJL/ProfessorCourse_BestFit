using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Helper;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProfessorCourse_BestFit.Controllers
{
    public class AccountController : Controller
    {
        private readonly ProfessorCourseBestFit1Entities1 _context;
        private readonly RolePermissions_DAL _sp;

        public AccountController()
        {
            _context = new ProfessorCourseBestFit1Entities1();
            _sp = new RolePermissions_DAL();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {

            User dbObj = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (dbObj != null)
            {
                if (dbObj.isDeleted == true && dbObj.Email != "admin@usal")
                {
                    ModelState.AddModelError("", "Email or Password is not valid");
                    return View(model);

                }
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is not valid");
                return View(model);
            }


            // get PasswordSalt from Databse as array of bytes
            byte[] PasswordSalt = Convert.FromBase64String(dbObj.PasswordSalt);

            // convert Password inserted by user from string into array of bytes
            byte[] Password = Encoding.UTF8.GetBytes(model.Password);

            // Hash the inserted password with salt
            byte[] HashedPassword = CryptoService.ComputeHMAC256(Password, PasswordSalt);

            // Convert password from byte array to string
            string stringPassword = Convert.ToBase64String(HashedPassword);

            // Get the original password from Database
            string dbPassword = dbObj.Password;

            // check if password is the same
            if (dbPassword != stringPassword)
            {
                ModelState.AddModelError("", "Email or Password is not valid");
                return View(model);
            }

            Session["fullname"] = dbObj.FirstName.ToUpper() + " " + dbObj.LastName.ToUpper();
            Session["image"] = dbObj.ImageUrl.Substring(dbObj.ImageUrl.IndexOf("/"));
            // get Id of user
            int roleId = dbObj.RoleId;
            // get roles for this user
            string permissions = dbObj.Uid.ToString() + ",";
            permissions += rolePermissionsName(roleId);

            // check if user select remember me option
            double timeOut = model.RememberMe ? 43200 : 30; // 525600 min = 1  year
            // create Forms Authentication Tickets
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, permissions, DateTime.Now, DateTime.Now.AddMinutes(timeOut), false, "");
            // encrypt Ticket
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            // Add encrypted ticket to httpCookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            // Add Cookie
            Response.Cookies.Add(cookie);
            Session["Language"] = "en";
            ViewBag.Uid = dbObj.Uid;
            return RedirectToAction("Index", "Home");

        }


        [NonAction]
        public bool IsEmailExist(string emailID)
        {


            var dbEmails = _context.Users.Where(e => e.Email == emailID).FirstOrDefault();
            // dbEmails != null => true, otherwise it is false
            return dbEmails != null;

        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }

        public string rolePermissionsName(int? id)
        {
            List<PermissionsViewModel> permissionViewModels = _sp.GetPermissionsByRoleId((int)id);
            string permissions = "";
            foreach (PermissionsViewModel item in permissionViewModels)
            {
                if (item.IsActive == false)
                {
                    permissions += item.PName + ",";
                }

            }

            return permissions;

        }

        [HttpPost]
        public JsonResult Languages(int isEnglish, string path)
        {
            if (isEnglish == 1)
            {
                Session["Language"] = "en";
                return Json(new
                {
                    redirectUrl = path,
                    isRedirect = true
                });
            }
            else
            {
                Session["Language"] = "ar";
                return Json(new
                {
                    redirectUrl = path,
                    isRedirect = true
                });
            }
        }
    }


}