using FastService.Models.Login;
using Model.Model;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace FastService.Controllers
{
    public class HomeController : BaseController
    {
        public FastServiceEntities _dbContext { get; set; }

        public HomeController()
        {
            _dbContext = new FastServiceEntities();
        }

        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(CurrentUserEmail))
            {
                return this.RedirectToAction("Index", new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index"
                }));
            }

            var model = (from x in _dbContext.UsuarioRol
                         join u in _dbContext.Usuario on x.UserId equals u.UserId
                         join r in _dbContext.Role on x.RolId equals r.RolId
                         where u.Email == CurrentUserEmail
                         select new MenuModel()
                         {
                             Nombre = u.Nombre,
                             Apellido = u.Apellido,
                             DefaultController = r.DefaultController,
                             DefaultAction = r.DefaultAction,
                             Roles = (from ur in _dbContext.UsuarioRol
                                      join ro in _dbContext.Role on ur.RolId equals ro.RolId
                                      where ur.UserId == u.UserId
                                      select ro.Nombre).ToList(),
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

            model.UserId = CurrentUserId; 
            CurrentUserRoles = model.Roles;
            InitializeViewBag();

            return View(model);
        }
    }
}