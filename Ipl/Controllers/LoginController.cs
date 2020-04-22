using Ipl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ipl.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private incedoEntities db = new incedoEntities();

        public string Username { get; private set; }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Login()
        {
            login model = new login() { username = Username };
            if (Request.Cookies["Login"] != null)
            {
                model.username = Request.Cookies["Login"].Values["username"];
                model.userpwd = Request.Cookies["Login"].Values["userpwd"];
            }
            return View(model);
        }



        [HttpPost, ValidateInput(false)]
        public ActionResult Login(login log)
        {
            if (ModelState.IsValid)
            {
                using (incedoEntities db = new incedoEntities())
                {
                    var result = db.logins.SingleOrDefault(x => x.username == log.username && x.userpwd == log.userpwd);
                    if (result != null)
                    {
                        FormsAuthentication.SetAuthCookie(log.username, log.RememberMe);
                        Session["username"] = log.username;
                        Session["Userpwd"] = log.userpwd;
                        if (log.RememberMe)
                        {
                            HttpCookie cookie = new HttpCookie("login");
                            cookie.Values.Add("username", log.username);
                            cookie.Values.Add("userpwd", log.userpwd);
                            cookie.Expires = DateTime.Now.AddDays(15);
                            Response.Cookies.Add(cookie);
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login data is incorrect!");
                    }
                }
            }
            return View(log);
        }



        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(login model)
        {
            db.logins.Add(model);
            db.SaveChanges();
            return RedirectToAction("Login", "Login");

        }

       
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}





