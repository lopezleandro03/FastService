using NLog;
using System;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class BaseController : Controller
    {
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