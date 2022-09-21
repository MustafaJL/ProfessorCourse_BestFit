using ProfessorCourse_BestFit.CustomSecurity;
using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Helper;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ProfessorCourse_BestFit.Controllers
{
    public class UserController : Controller
    {

        private readonly ProfessorCourseBestFit1Entities1 _context;
        private readonly UserKeywords_DAL _sp;

        public UserController()
        {
            _context = new ProfessorCourseBestFit1Entities1();
            _sp = new UserKeywords_DAL();
        }
        // GET: Professor

        [CustomAuthorization(Permissions: "Read")]

        public ActionResult Index()
        {
            var userList = _context.Users.Include(m => m.Role).Where(x => x.isDeleted == false).ToList();
            return View(userList);



        }
        // [CustomAuthorization(Permissions: "Create")]


        [CustomAuthorization(Permissions: "Create")]
        public ActionResult Create()
        {
            var roles = _context.Roles.Where(x => x.isDeleted == false).ToList();
            var viewModel = new UserRolesViewModel
            {
                User = new UserViewModel(),
                Roles = roles
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRolesViewModel model)
        {
            var image = "";
            if (model.User.ImageFile == null)
            {
                image = "~/Content/Images/Users/account.png";
            }

            else if (model.User.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.User.ImageFile.FileName);
                string extension = Path.GetExtension(model.User.ImageFile.FileName);

                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                image = "~/Content/Images/Users/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Content/Images/Users/"), fileName);

                model.User.ImageFile.SaveAs(fileName);
            }
            User user = new User();

            var isExist = IsEmailExist(model.User.Email, 0);
            if (isExist)
            {
                var roles = _context.Roles.Where(x => x.isDeleted == false).ToList();

                ViewBag.Error = "Email already Exist";
                ViewBag.gender = model.User.Gender;
                ViewBag.id = model.User.Id;
                var viewModel = new UserRolesViewModel
                {
                    User = model.User,
                    Roles = roles
                };
                return View(viewModel);
            }

            user.FirstName = model.User.FirstName;
            user.MiddleName = model.User.MiddleName;
            user.LastName = model.User.LastName;

            user.Email = model.User.Email;
            user.DateOfBirth = model.User.DateOfBirth;
            user.CreatedOn = DateTime.Now;

            user.Gender = model.User.Gender;
            user.Phone = model.User.PhoneNumber;
            user.ImageUrl = image;

            user.Education = model.User.Education;
            user.Address = model.User.Address;


            user.RoleId = model.User.RoleId;
            #region // Generate a salt
            var userSalt = CryptoService.GenerateSalt();
            user.PasswordSalt = Convert.ToBase64String(userSalt);
            #endregion

            #region // Hash the password using PasswordSalt
            var userPasswor = Encoding.UTF8.GetBytes("S@s123456");
            var hmac = CryptoService.ComputeHMAC256(userPasswor, userSalt);
            user.Password = Convert.ToBase64String(hmac);
            #endregion
            _context.Users.Add(user);
            _context.SaveChanges();


            //Session["fullname"] = user.FirstName.ToUpper() + " " + user.LastName.ToUpper();
            //Session["image"] = user.ImageUrl.Substring(user.ImageUrl.IndexOf("/"));

            return RedirectToAction("Index");
        }




        public ActionResult Update(int? id)
        {
            if (id == 0 || id == null)
            {
                return HttpNotFound();
            }

            var user = _context.Users.Where(u => u.isDeleted == false).SingleOrDefault(u => u.Uid == id);

            var model = new UserViewModel
            {
                Id = id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Address = user.Address,
                Education = user.Education,
                PhoneNumber = user.Phone,
                RoleId = user.RoleId


            };

            var roles = _context.Roles.Where(x => x.isDeleted == false).ToList();
            var viewModel = new UserRolesViewModel
            {
                User = model,
                Roles = roles
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserRolesViewModel model)
        {
            var isExist = IsEmailExist(model.User.Email, model.User.Id);
            if (isExist)
            {
                var roles = _context.Roles.Where(x => x.isDeleted == false).ToList();

                ViewBag.Error = "Email already Exist";
                ViewBag.gender = model.User.Gender;
                ViewBag.id = model.User.Id;
                var viewModel = new UserRolesViewModel
                {
                    User = model.User,
                    Roles = roles
                };
                return View(viewModel);
            }

            var image = "";
            if (model.User.ImageFile == null)
            {
                image = "~/Content/Images/Users/account.png";
            }

            else if (model.User.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.User.ImageFile.FileName);
                string extension = Path.GetExtension(model.User.ImageFile.FileName);

                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                image = "~/Content/Images/Users/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Content/Images/Users/"), fileName);

                model.User.ImageFile.SaveAs(fileName);
            }

            var userDb = _context.Users.SingleOrDefault(u => u.Uid == model.User.Id);


            userDb.FirstName = model.User.FirstName;
            userDb.LastName = model.User.LastName;
            userDb.Email = model.User.Email;
            userDb.DateOfBirth = model.User.DateOfBirth;
            userDb.CreatedOn = DateTime.Now;
            userDb.Gender = model.User.Gender;
            userDb.Phone = model.User.PhoneNumber;
            userDb.MiddleName = model.User.MiddleName;
            userDb.Address = model.User.Address;
            userDb.Education = model.User.Education;
            userDb.RoleId = model.User.RoleId;

            userDb.ImageUrl = model.User.ImageFile == null ? userDb.ImageUrl : image;



            // Save object to the Database
            _context.SaveChanges();

            //Session["fullname"] = userDb.FirstName.ToUpper() + " " + userDb.LastName.ToUpper();
            //Session["image"] = userDb.ImageUrl.Substring(userDb.ImageUrl.IndexOf("/"));

            return RedirectToAction("Index");
        }





        [HttpPost]
        public JsonResult Delete(int id)
        {

            var model = _context.Users.SingleOrDefault(x => x.Uid == id);
            model.isDeleted = true;
            _context.SaveChanges();
            return Json(new { success = true });


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
        public bool IsEmailExist(string emailID, int? UId)
        {


            var dbEmails = _context.Users.Where(e => e.Email == emailID).FirstOrDefault(u => u.Uid != UId);
            // dbEmails != null => true, otherwise it is false
            return dbEmails != null;

        }

        [HttpGet]
        public ActionResult Profile(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Uid == id);
            if (id.ToString() != User.Identity.Name.Substring(0, User.Identity.Name.IndexOf(',')))
            {
                return RedirectToAction("UnAuthorized", "Account");
            }

            var userViewModel = new UserViewModel
            {
                Id = id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                PhoneNumber = user.Phone,
                ImageUrl = user.ImageUrl,
                Address = user.Address,
                Education = user.Education,


            };




            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Profile(UserViewModel model)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                return HttpNotFound();
            }

            var image = "";
            if (model.ImageFile == null)
            {
                image = "~/Content/Images/Users/account.png";
            }
            else if (model.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName);

                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                image = "~/Content/Images/Users/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Content/Images/Users/"), fileName);

                model.ImageFile.SaveAs(fileName);
            }

            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;
            user.Gender = model.Gender;
            user.Phone = model.PhoneNumber;
            user.Address = model.Address;
            user.Education = model.Education;
            user.ImageUrl = model.ImageFile == null ? user.ImageUrl : image;


            _context.SaveChanges();

            Session["fullname"] = user.FirstName.ToUpper() + " " + user.LastName.ToUpper();
            Session["image"] = user.ImageUrl.Substring(user.ImageUrl.IndexOf("/"));
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public JsonResult DeleteImage(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Uid == id);
            user.ImageUrl = "~/Content/Images/Users/account.png";
            _context.SaveChanges();

            Session["image"] = user.ImageUrl.Substring(user.ImageUrl.IndexOf("/"));
            return Json(new { success = true });


        }




        public ActionResult userKeywords(int? id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Uid == id);

            var keywords = _sp.GetAllKeywordsIncludesMatchingByUserId((int)id);

            UserKeywordsViewModel userKeywordsView = new UserKeywordsViewModel
            {

                User = user,
                Keywords = keywords,


            };
            return View(userKeywordsView);
        }

        [HttpPost]
        public JsonResult userKeywords(int userId, string[] keywords)
        {
            string keywordsString = "";
            if (keywords != null && keywords.Length > 0)
            {
                keywordsString = string.Join(",", keywords);
            }
            else
            {
                keywordsString = "0";
            }

            var a = _sp.UpdateUserKeyword(userId, keywordsString);
            return Json(new { success = true });
        }
    }
}