using FastService.Models.Login;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Backend;

namespace FastService.Controllers
{
    public class HomeController : Controller
    {
        public FastServiceEntities _dbContext { get; set; }

        public HomeController()
        {
            _dbContext = new FastServiceEntities();
        }

        public ActionResult Index()
        {
            bool flag = string.IsNullOrEmpty(System.Web.HttpContext.Current.Session["USER"] as string);
            ActionResult result;

            if (flag) //Just for testing
            {
                result = this.RedirectToAction("Index", new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index"
                }));
            }
            else
            {
                var user = System.Web.HttpContext.Current.Session["USER"] as string;


                if (!CommonUtility.IsDevelopmentServer())
                {
                    var model = new MenuModel()
                    {
                        Nombre = "Leandro",
                        Apellido = "Lopez",
                        DefaultAction = "Create",
                        DefaultController = "Payment",
                        MenuItems = new List<MenuItemModel>()
                        { new MenuItemModel()
                            {
                                Action = null, Controller = null,DisplayName = "Mercado Pago",MenuId = 1,MenuName = "MercadoPago",ParentId = null
                            },
                            new MenuItemModel()
                            {
                                Action = "Create", Controller = "Payment",DisplayName = "Nuevo Pago",MenuId = 1,MenuName = "NuevoPago",ParentId = 1
                            }
                    }};

                    result = this.View(model);
                }
                else
                {

                    var model = (from x in _dbContext.UsuarioRol
                                 join u in _dbContext.Usuario on x.UserId equals u.UserId
                                 join r in _dbContext.Role on x.RolId equals r.RolId
                                 where u.Email == user
                                 select new MenuModel()
                                 {
                                     Nombre = u.Nombre,
                                     Apellido = u.Apellido,
                                     DefaultController = r.DefaultController,
                                     DefaultAction = r.DefaultAction,
                                     MenuItems = (from rm in _dbContext.RoleMenu
                                                  join i in _dbContext.ItemMenu on rm.ItemMenuId equals i.ItemMenuId
                                                  where rm.RolId == x.RolId
                                                  select new MenuItemModel()
                                                  {
                                                      MenuId = i.ItemMenuId,
                                                      MenuName = i.Name,
                                                      DisplayName = i.Name,
                                                      Controller = i.Controlador,
                                                      Action = i.Accion,
                                                      ParentId = i.ItemMenuPadreId,
                                                      Icon = i.Icon
                                                  }).ToList()
                                 }).ToList().FirstOrDefault();

                    result = this.View(model);

                }

            }

            return result;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}