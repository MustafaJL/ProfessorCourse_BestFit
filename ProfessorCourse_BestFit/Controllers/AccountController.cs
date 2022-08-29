using ProfessorCourse_BestFit.CustomSecurity;
using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Helper;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProfessorCourse_BestFit.Controllers
{
    public class AccountController : Controller
    {
        private readonly ProfessorCourseBestFitEntities _context;
        User_DAL userDAL = new User_DAL();

        public AccountController()
        {
            _context = new ProfessorCourseBestFitEntities();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            // check if email exist in data base
            if (!_context.Users.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("", "Email or Password is not valid");
                return View(model);
            }
            // get all data of this email
            User dbObj = _context.Users.FirstOrDefault(x => x.Email == model.Email);

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
            // get Id of user
            int UserId = dbObj.Uid;
            // get roles for this user
            string Roles = userDAL.UserRoleNames(UserId);

            // check if user select remember me option
            double timeOut = model.RememberMe ? 43200 : 30; // 525600 min = 1  year
            // create Forms Authentication Tickets
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, Roles, DateTime.Now, DateTime.Now.AddMinutes(timeOut), false, "");

            // encrypt Ticket
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            // Add encrypted ticket to httpCookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            // Add Cookie
            Response.Cookies.Add(cookie);


            return RedirectToAction("Index", "Home");

        }
        [CustomAuthorization("Admin")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {

                #region // Is Email already exist
                var isExist = IsEmailExist(model.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already Exist");
                    return View(model);
                }
                #endregion

                User user = new User();

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.DateOfBirth = model.DateOfBirth;
                user.CreatedOn = DateTime.Now;

                #region // Generate a salt
                var userSalt = CryptoService.GenerateSalt();
                user.PasswordSalt = Convert.ToBase64String(userSalt);
                #endregion

                #region // Hash the password using PasswordSalt
                var userPasswor = Encoding.UTF8.GetBytes(model.Password);
                var hmac = CryptoService.ComputeHMAC256(userPasswor, userSalt);
                user.Password = Convert.ToBase64String(hmac);
                #endregion

                // Save object to the Database
                _context.Users.Add(user);
                _context.SaveChanges();



                return View();
            }
            else
            {
                return View();
            }
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
            Session.Abandon();
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }
    }


}