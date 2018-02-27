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
            get { return System.Web.HttpContext.Current.Session["USERMAIL"] == null ? null : System.Web.HttpContext.Current.Session["USERMAIL"].ToString(); }
            set { System.Web.HttpContext.Current.Session["USERMAIL"] = value; }
        }

        public int CurrentUserId
        {
            get { return System.Web.HttpContext.Current.Session["USERID"] == null ? 0 : Convert.ToInt16(System.Web.HttpContext.Current.Session["USERID"]); }
            set { System.Web.HttpContext.Current.Session["USERID"] = value; }
        }

        public List<string> CurrentUserRoles
        {
            get { return System.Web.HttpContext.Current.Session["USERROLES"] == null ? null : (List<string>)System.Web.HttpContext.Current.Session["USERROLES"]; }
            set { System.Web.HttpContext.Current.Session["USERROLES"] = value; }
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