using FastService.Controllers;
using Model.Model;
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
            return View();
        }

        public ActionResult IndexRetryLogin(string loginStatus)
        {
            ViewBag.LoginStatus = loginStatus ?? string.Empty;
            return this.View("Index");
        }

        public ActionResult Authenticate(FormCollection collection)
        {
            var Email = collection.Get("Email").ToString();
            var Contraseña = collection.Get("Contraseña").ToString();

            var user = from x in _dbContext.Usuario
                       where x.Email == Email
                       && x.Contraseña == Contraseña
                       select x;

            if (user.Any())
            {
                CurrentUserEmail = user.First().Email;
                CurrentUserId = user.First().UserId;

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

                //return this.RedirectToAction("Index");
            }
        }

        public ActionResult LogOut()
        {
            CurrentUserEmail = string.Empty;
            CurrentUserId = 0;

            var result = this.RedirectToAction("Index", new RouteValueDictionary(new
            {
                controller = "Login",
                action = "Index"
            }));

            return result;
        }
    }
}
