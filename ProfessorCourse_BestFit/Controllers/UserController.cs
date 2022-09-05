using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Helper;
using ProfessorCourse_BestFit.Models;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class UserController : Controller
    {

        private readonly ProfessorCourseBestFitEntities _context;
        User_DAL userDAL = new User_DAL();

        public UserController()
        {
            _context = new ProfessorCourseBestFitEntities();
        }

        // GET: Professor

        public ActionResult Index()
        {
            var userList = _context.Users.Where(x => x.deleted == false).ToList();
            return View(userList);
        }
        public ActionResult Upsert_User(int? id)
        {
            ViewBag.id = id;
            if (id == 0 || id == null)
            {
                return View();
            }
            var user = _context.Users.SingleOrDefault(x => x.Uid == id);

            var model = new UserViewModel
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = (DateTime)user.DateOfBirth,
                Gender = user.Gender,
                PhoneNumber = user.Phone,


            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert_User(UserViewModel model)
        {
            if (ModelState.IsValid)
            {

                #region // Is Email already exist
                var isExist = IsEmailExist(model.Email);
                if (isExist)
                {
                    ViewBag.Error = "Email already Exist";
                    return View(model);
                }
                #endregion

                User user = new User();

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.DateOfBirth = model.DateOfBirth;
                user.CreatedOn = DateTime.Now;
                user.Gender = model.Gender;
                user.Phone = model.PhoneNumber;
                user.MiddleName = model.MiddleName;

                #region // Generate a salt
                var userSalt = CryptoService.GenerateSalt();
                user.PasswordSalt = Convert.ToBase64String(userSalt);
                #endregion

                #region // Hash the password using PasswordSalt
                var userPasswor = Encoding.UTF8.GetBytes("S@s123456");
                var hmac = CryptoService.ComputeHMAC256(userPasswor, userSalt);
                user.Password = Convert.ToBase64String(hmac);
                #endregion

                // Save object to the Database
                _context.Users.Add(user);
                _context.SaveChanges();



                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }



        [HttpPost]
        public JsonResult Delete(int id)
        {
            var rolesUser = _context.UserRoles.Where(x => x.UserId == id).ToList();
            if (!(rolesUser.Count > 0))
            {
                var model = _context.Users.SingleOrDefault(x => x.Uid == id);
                model.deleted = true;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }


        [HttpPost]
        public JsonResult ResetPassword(int id)
        {
            var model = _context.Users.Find(id);

            // get PasswordSalt from Databse as array of bytes
            byte[] PasswordSalt = Convert.FromBase64String(model.PasswordSalt);

            // convert Password inserted by user from string into array of bytes
            byte[] Password = Encoding.UTF8.GetBytes("S@s123456");

            // Hash the inserted password with salt
            byte[] HashedPassword = CryptoService.ComputeHMAC256(Password, PasswordSalt);

            // Convert password from byte array to string
            string stringPassword = Convert.ToBase64String(HashedPassword);

            // Update the original password from Database
            model.Password = stringPassword;





            _context.SaveChanges();
            return Json(new { success = true });
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {


            var dbEmails = _context.Users.Where(e => e.Email == emailID).FirstOrDefault();
            // dbEmails != null => true, otherwise it is false
            return dbEmails != null;

        }


    }
}