using NLog;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

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
                else if (GetCookie("USERROLES") != null)
                {
                    System.Web.HttpContext.Current.Session["USERROLES"] = GetCookie("USERROLES");
                    return (List<string>)System.Web.HttpContext.Current.Session["USERROLES"];
                }

                return null;
            }
            set
            {
                System.Web.HttpContext.Current.Session["USERROLES"] = value;
                SetCookie("USERROLES", value.ToString());
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
            Exception e = filterContext.Exception;
            //var logger = LogManager.GetCurrentClassLogger();
            ////var logInfo = new LogEventInfo();
            ////logInfo.Properties["EventDateTime"] = DateTime.Now;
            ////logInfo.Properties["EventLevel"] = LogLevel.Error;
            ////logInfo.Properties["UserName"] = CurrentUserEmail ?? string.Empty;
            ////logInfo.Properties["EventMessage"] = e.Message ?? string.Empty;
            //////logInfo.Properties["ErrorSource"] = e.Source ;
            //////logInfo.Properties["ErrorClass"] = e.;
            //////logInfo.Properties["ErrorMethod"] = ;
            ////logInfo.Properties["ErrorMessage"] = e.StackTrace ?? string.Empty;
            ////logInfo.Properties["InnerErrorMessage"] = e.InnerException.Message ?? string.Empty;

            ////logger.Log(LogLevel.Error, logInfo);
            //logger.Log(LogLevel.Error, e, e.StackTrace);

            //filterContext.ExceptionHandled = true;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var result = new { Success = "false", Message = e.Message };
                filterContext.Result = Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                filterContext.Result = new PartialViewResult()
                {
                    ViewName = "Error"
                };
            }
        }
    }


}