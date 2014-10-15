using Login.Client.Models;
using Login.Data;
using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cryptography;
using System.Net;
using System.Web.Script.Serialization;

namespace Login.Client.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// The repository that will store the user's accounts.
        /// </summary>
        private IFakeRepository<ApplicationUser> repository;
        /// <summary>
        /// The hasher used for hashing passwords
        /// </summary>
        private RijndaelAlg hasher;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="repo">The repository that will store the user's accounts.</param>
        public AccountController(IFakeRepository<ApplicationUser> repo)
        {
            this.repository = repo;
            hasher = new RijndaelAlg();
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(UserViewModel userData)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser existingUser = repository.All()
                    .FirstOrDefault(user => user.UserName == userData.UserName);
                if (existingUser == null)
                {
                    ModelState.AddModelError("UserName", "Username does not exist");
                }
                else if (existingUser.Password != hasher.Encrypt(userData.Password))
                {
                    ModelState.AddModelError("UserName", "Incorrect username/password");
                    ModelState.AddModelError("Password", "");
                }
                else
                {
                    //Easier for testing
                    return Json(new
                    {
                        valid = true,
                        url = string.Format("/Account/UserPage?Id={0}"
                            , existingUser.Id)
                    });
                    //return Json(new { valid=true, url = Url.Action("UserPage", new { id = existingUser.Id }) });
                }
            }

            string jsonErrors = SerialiseErrors();

            return Json(new { valid = false, validationErrors = jsonErrors });
        }

        public ActionResult LoginUser(UserViewModel userData)
        {
            ViewBag.isHeshed = true;
            return View("Login", userData);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SignUp(UserViewModel userData)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser existingUser = repository.All()
                    .FirstOrDefault(user => user.UserName == userData.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "Unable to sign up");
                }
                else
                {
                    ApplicationUser newUser = new ApplicationUser
                    {
                        UserName = userData.UserName,
                        Password = hasher.Encrypt(userData.Password)
                    };
                    repository.Save(newUser);

                    //Easier for testing
                    return Json(new
                    {
                        valid = true,
                        url = string.Format("/Account/LoginUser?UserName={0}&Password={1}"
                            , HttpUtility.UrlEncode(newUser.UserName), HttpUtility.UrlEncode(newUser.Password))
                    });
                    //return Json(new { valid = true,url = Url.Action("LoginUser", userData) });
                }
            }

            string jsonErrors = SerialiseErrors();

            return Json(new { valid = false, validationErrors = jsonErrors });
        }

        public ActionResult UserPage(int? id)
        {
            if (id == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "The id can't be null!");
            }
            ApplicationUser currentUser = repository.All()
                .FirstOrDefault(user => user.Id == id);
            if (currentUser == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "There is no user with this id!");
            }

            UserDisplayViewModel userModel = new UserDisplayViewModel
            {
                UserName = currentUser.UserName
            };

            return View(userModel);
        }

        /// <summary>
        /// Serialises the ModelState errors. <see cref="System.Web.Mvc.ModelStateDictionary"/>
        /// </summary>
        /// <returns>Json string with information from the ModelState errors </returns>
        /// 
        private string SerialiseErrors()
        {
            Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
            foreach (var modelState in ModelState)
            {
                errors.Add(modelState.Key, new List<string>());
                foreach (var item in modelState.Value.Errors)
                {
                    errors[modelState.Key].Add(item.ErrorMessage);
                }
            }
            var jsonErrors = new JavaScriptSerializer().Serialize(errors);
            return jsonErrors;
        }
    }
}