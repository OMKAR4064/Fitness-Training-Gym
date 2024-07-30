using BeFir.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BeFir.Controllers
{
    public class RegisterController : Controller
    {
        // GET: RegisterController
        public ActionResult AllFormList()
        {
            string s = HttpContext.Session.GetString("sessinoName");

            List<Register> allFormsData = Register.GetAllForms();

            if (s == null )
            {
                
                    return RedirectToAction("RegistrationForm");
                
            }
            else
            {
                if (s.Equals("Admin"))
                {
                    if (allFormsData.IsNullOrEmpty())
                    {
                        ViewBag.message = "No record found";
                        return View();
                    }
                    else
                    {

                        return View(allFormsData);
                    }
                }
                else
                {
                    return RedirectToAction("RegistrationForm");
                }



            }

        }

        // GET: RegisterController/Details/5
        public ActionResult SingleFormDetails(int id)
        {

            Register singleData = Register.getSingleFormData(id);

            string s = HttpContext.Session.GetString("sessinoName");


            if (s == null)
            {

                return RedirectToAction("RegistrationForm");

            }
            else
            {
                if (s.Equals("Admin"))
                {
                    if (singleData == null)
                    {
                        ViewBag.message = "No record found";
                        return View();
                    }
                    else
                    {

                        return View(singleData);
                    }
                }
                else
                {
                    return RedirectToAction("RegistrationForm");
                }
            }
        }
        // GET: RegisterController/Create
        public ActionResult RegistrationForm()
        {
            string s = HttpContext.Session.GetString("sessinoName");
            if (s!=null && !s.Equals("Admin"))
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // POST: RegisterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrationForm(Register register)
        {
            try
            {

                Register.insertForm(register);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterController/Edit/5
        public ActionResult EditForm(int id)
        {

            Register singleData = Register.getSingleFormData(id);


           
            string s = HttpContext.Session.GetString("sessinoName");


            if (s == null)
            {

                return RedirectToAction("RegistrationForm");

            }
            else
            {
                if (s.Equals("Admin"))
                {
                    if (singleData == null)
                    {
                        ViewBag.message = "No record found";
                        return View();
                    }
                    else
                    {

                        return View(singleData);
                    }
                }
                else
                {
                    return RedirectToAction("RegistrationForm");
                }
            }

        }

        // POST: RegisterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForm(int id, Register register)
        {
            try
            {
                Register.updateForm(register);

                return RedirectToAction("AllFormList");
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterController/Delete/5
        public ActionResult DeleteForm(int id)
        {
            Register singleData = Register.getSingleFormData(id);
            string s = HttpContext.Session.GetString("sessinoName");


            if (s == null)
            {

                return RedirectToAction("RegistrationForm");

            }
            else
            {
                if (s.Equals("Admin"))
                {
                    if (singleData == null)
                    {
                        ViewBag.message = "No record found";
                        return View();
                    }
                    else
                    {

                        return View(singleData);
                    }
                }
                else
                {
                    return RedirectToAction("RegistrationForm");
                }
            }

        }

        // POST: RegisterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(int id, Register register)
        {
            try
            {
                Register.deleteForm(id);

                return RedirectToAction("AllFormList");
            }
            catch
            {
                return View();
            }
        }
    }
}
