using NLog;
using System;
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

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            //Log Exception e
            var logger = LogManager.GetCurrentClassLogger();

            logger.Log(LogLevel.Error, e, e.Message);

            filterContext.ExceptionHandled = true;

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