using BeFir.Models;
using FitnessGym.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Diagnostics;

namespace FitnessGym.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();

        }

        public ActionResult Tranner()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            try
            {

                if (BeFir.Models.Contact.insertContactData(contact))
                {
                    Contact c = new Contact
                    {
                        Name = "",
                        Email = "",
                        PhoneNumber = "",
                        Message = ""
                    };
                    ViewBag.CMsg = "Your Responce is Submited";
                    return View(c);
                }
                else
                {
                    ViewBag.CMsg = "an error occurred during submitting ";
                    return View();
                }



            }
            catch
            {
                ViewBag.CMsg = "an error occurred during submitting ";
                return View();
            }
            
        }

        //public ActionResult Registration()
        //{
        //    return View();

        //}
        [Route("login")]
        public ActionResult Login()
        {
            ViewBag.UserMsg = TempData["UserMsg"];
            return View();
        }
        [Route("LogOut")]
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();

            // Optionally, remove the session cookie
            HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
            return RedirectToAction("Index");
        }

        //// NEw Added
        public ActionResult Registration()
        {
            ViewBag.Msg = TempData["Msg"];
            return View();
        }
        /////

        [Route("login")]
        [HttpPost]
        public ActionResult Login(IFormCollection collection)
        {
            var Username = collection["Admin.Username"];
            var Password = collection["Admin.Password"];
            Console.WriteLine("Home Route got it " + Username + Password);
            Admin admin = Admin.loginAdmin(Username, Password);
            if (ModelState.IsValid)
            {
                if (admin != null)
                {
                    HttpContext.Session.SetString("sessinoName", admin.Username);

                    return RedirectToAction("AllFormList", "Register");
                }

                // ViewBag.message("Login Sucess");
                ModelState.Clear();
            }
            ViewBag.AdMsg = "Please Enter Valid Username or Password";
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
