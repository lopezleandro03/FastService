using Model.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace FastService.Controllers
{
    public class BaseController : Controller
    {
        public string CurrentUserEmail
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["USERMAIL"] != null)
                {
                    return System.Web.HttpContext.Current.Session["USERMAIL"].ToString();
                }
                else if (GetCookie("USERMAIL") != null)
                {
                    System.Web.HttpContext.Current.Session["USERMAIL"] = GetCookie("USERMAIL");
                    return System.Web.HttpContext.Current.Session["USERMAIL"].ToString();
                }
                return null;
            }
            set
            {
                System.Web.HttpContext.Current.Session["USERMAIL"] = value;
                SetCookie("USERMAIL", value);
            }
        }

        public string CurrentUserLogin
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["USERLOGIN"] != null)
                {
                    return System.Web.HttpContext.Current.Session["USERLOGIN"].ToString();
                }
                else if (GetCookie("USERLOGIN") != null)
                {
                    System.Web.HttpContext.Current.Session["USERLOGIN"] = GetCookie("USERLOGIN");
                    return System.Web.HttpContext.Current.Session["USERLOGIN"].ToString();
                }
                return null;
            }
            set
            {
                System.Web.HttpContext.Current.Session["USERLOGIN"] = value;
                SetCookie("USERLOGIN", value);
            }
        }

        public int CurrentUserId
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["USERID"] != null)
                {
                    return Convert.ToInt16(System.Web.HttpContext.Current.Session["USERID"]);
                }
                else if (GetCookie("USERID") != null)
                {

                    System.Web.HttpContext.Current.Session["USERID"] = GetCookie("USERID");
                    return Convert.ToInt16(System.Web.HttpContext.Current.Session["USERID"]);
                }

                return 0;
            }
            set
            {
                System.Web.HttpContext.Current.Session["USERID"] = value;
                SetCookie("USERID", value.ToString());
            }
        }

        public List<string> CurrentUserRoles
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["USERROLES"] != null)
                {
                    return (List<string>)System.Web.HttpContext.Current.Session["USERROLES"];
                }
                else if (CurrentUserId != null)
                {
                    using (var db = new FastServiceEntities())
                    {
                        return (from x in db.UsuarioRol
                                where x.UserId == CurrentUserId
                                select x.Role.Nombre).ToList();
                    }
                }

                return null;
            }
            set
            {
                System.Web.HttpContext.Current.Session["USERROLES"] = value;
            }
        }


        public string GetCookie(string cookie)
        {
            if (Request.Cookies[cookie] != null)
            {
                return Request.Cookies[cookie].Value.ToString();
            }

            return null;
        }

        public void SetCookie(string cookie, string value)
        {
            Response.Cookies[cookie].Value = value;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var logger = LogManager.GetCurrentClassLogger();

            if (!filterContext.HttpContext.Request.IsLocal)
            {
                logger.Error(filterContext.Exception);
            }

            filterContext.ExceptionHandled = true;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                throw filterContext.Exception; //Throw exception for ajax failure on client side
                //var result = new { Success = "false", Message = filterContext.Exception.Message };
                //filterContext.Result = Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                filterContext.Result = new PartialViewResult()
                {
                    ViewName = "Error"
                };
            }
        }

        public void InitializeViewBag()
        {
            using (var _db = new FastServiceEntities())
            {

                ViewBag.ListaMarcas = (from y in _db.Marca
                                       where y.nombre.Trim() != string.Empty
                                       && y.activo
                                       select new SelectListItem()
                                       {
                                           Text = y.nombre,
                                           Value = y.MarcaId.ToString()
                                       }).OrderBy(y => y.Text).ToList();

                ViewBag.ListaComercio = (from y in _db.Comercio
                                         where y.Code.Trim() != string.Empty
                                         && y.activo
                                         select new SelectListItem()
                                         {
                                             Text = y.Code,
                                             Value = y.ComercioId.ToString()
                                         }).OrderBy(y => y.Text).ToList();

                ViewBag.ListaTipoDispositivo = (from y in _db.TipoDispositivo
                                                where y.nombre.Trim() != string.Empty
                                                && y.activo
                                                select new SelectListItem()
                                                {
                                                    Text = y.nombre,
                                                    Value = y.TipoDispositivoId.ToString()
                                                }).OrderBy(y => y.Text).ToList();

                ViewBag.ListaTecnicos = (from u in _db.Usuario
                                         join ur in _db.UsuarioRol on u.UserId equals ur.UserId
                                         where u.Activo && ur.Role.Nombre == "TECNICO"
                                         select new SelectListItem()
                                         {
                                             Text = u.Nombre,
                                             Value = u.UserId.ToString()
                                         }).OrderBy(y => y.Text).ToList();

                ViewBag.ListaResponsables = (from y in _db.Usuario
                                             where y.Activo
                                             select new SelectListItem()
                                             {
                                                 Text = y.Nombre,
                                                 Value = y.UserId.ToString()
                                             }).OrderBy(y => y.Text).ToList();
            }
        }
    }


}