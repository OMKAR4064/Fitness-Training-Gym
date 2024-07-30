using BeFir.Models;
using FitnessGym.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeFir.Controllers
{
    public class UsersController : Controller
    {
        // GET: UsersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("loginuser")]
        public ActionResult LoginUser()
        {
            return View();
        }
        [Route("LogOutUser")]
        public ActionResult LogOutUser()
        {
            HttpContext.Session.Clear();

            // Optionally, remove the session cookie
            HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
            return RedirectToAction("Index");
        }

        [Route("loginUser")]
        [HttpPost]
        public ActionResult loginUser(IFormCollection collection)
        {
            
            var Username = collection["User.Username"];
            var Password = collection["User.Password"];
            Console.WriteLine("USer Route got it " + Username + Password);

            BeFir.Models.User user = BeFir.Models.User.loginUser(Username, Password);

            //User user = User.loginUser(Username, Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                TempData["UserMsg"] = "Please Enter Valid Username or Password";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine(user.Username + " " +user.Password);
                HttpContext.Session.SetString("sessinoName", user.Username ?? string.Empty); // Ensure not null

                return RedirectToAction("Index", "Home");
            }

            //if (ModelState.IsValid)
            //{
            //    if (user != null )
            //    {
            //        HttpContext.Session.SetString("sessinoName", user.Username);

            //        return RedirectToAction("Index","Home");
            //    }

            //    // ViewBag.message("Login Sucess");
            //    ModelState.Clear();
            //}

            return RedirectToAction("Login", "Home");
        }
        [Route("userRegistration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult userRegistration(User user)
        {
            try
            {

                BeFir.Models.User.insertUser(user);
                return RedirectToAction("Login", "Home");

                //return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }


    }
}
