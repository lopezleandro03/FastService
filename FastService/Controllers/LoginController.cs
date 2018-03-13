using FastService.Controllers;
using Model.Model;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace EstudioCapra.Controllers
{
    public class LoginController : BaseController
    {
        public FastServiceEntities _dbContext { get; set; }

        public LoginController()
        {
            _dbContext = new FastServiceEntities();
        }

        public ActionResult Index()
        {
            HttpContext.Session.Abandon();
            return View();
        }

        public ActionResult IndexRetryLogin(string loginStatus)
        {
            ViewBag.LoginStatus = loginStatus ?? string.Empty;
            return this.View("Index");
        }

        public ActionResult Authenticate(FormCollection collection)
        {
            var Login = collection.Get("Email").ToString();
            var Contraseña = collection.Get("Contraseña").ToString();

            var user = from x in _dbContext.Usuario
                       where x.Email == Login
                          || x.Login == Login
                       && x.Contraseña == Contraseña
                       && x.Activo
                       select x;

            if (user.Any())
            {
                CurrentUserEmail = user.First().Email;
                CurrentUserId = user.First().UserId;
                CurrentUserLogin = user.First().Login;

                return this.RedirectToAction("Index", new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
            else
            {
                return this.RedirectToAction("IndexRetryLogin", new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "IndexRetryLogin",
                    loginStatus = "Login Failed"
                }));

            }
        }

        public ActionResult LogOut()
        {
            CurrentUserEmail = null;
            CurrentUserLogin = null;
            CurrentUserId = 0;

            RemoveCookie("USERLOGIN");

            var result = this.RedirectToAction("Index", new RouteValueDictionary(new
            {
                controller = "Login",
                action = "Index"
            }));

            return result;
        }

        private void RemoveCookie(string cookie)
        {
            if (Request.Cookies[cookie] != null)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
        }

    }
}
