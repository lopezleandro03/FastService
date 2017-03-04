using FastService.Models.Login;
using Model.Model;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace EstudioCapra.Controllers
{
    public class LoginController : Controller
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
            var user = from x in _dbContext.Usuario
                           where x.Email == collection.Get("Email").ToString() 
                           && x.Contraseña == collection.Get("Contraseña").ToString()
                           select x;

            if (user != null)
            {
                System.Web.HttpContext.Current.Session["USER"] = collection.Get("Email").ToString();

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

        //public ActionResult Authorize()
        //{
            //var user = System.Web.HttpContext.Current.Session["USER"] as string;

            //var model = (from x in _dbContext.UsuarioRol
            //             join u in _dbContext.Usuario on x.UserId equals u.UserId
            //             join r in _dbContext.Role on x.RolId equals r.RolId
            //             where u.Email == user
            //             select new MenuModel()
            //             {
            //                 Nombre = u.Nombre,
            //                 Apellido = u.Apellido,
            //                 DefaultController = r.DefaultController,
            //                 DefaultAction = r.DefaultAction,
            //                 MenuItems = (from i in r.ItemMenu
            //                              select new MenuItemModel()
            //                              {
            //                                  MenuId = i.ItemMenuId,
            //                                  MenuName = i.Name,
            //                                  DisplayName = i.Name,
            //                                  Controller = i.Controlador,
            //                                  Action = i.Accion,
            //                                  ParentId = i.ItemMenuPadreId
            //                              }).ToList()
            //             }).ToList().FirstOrDefault();

          //  return View("_Menu", model);
        //}

        public ActionResult LogOut()
        {
            System.Web.HttpContext.Current.Session["USER"] = string.Empty;

            var result = this.RedirectToAction("Index", new RouteValueDictionary(new
            {
                controller = "Login",
                action = "Index"
            }));

            return result;
        }
    }
}
