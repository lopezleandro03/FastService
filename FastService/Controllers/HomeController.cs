﻿using FastService.Models.Login;
using Model.Model;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

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