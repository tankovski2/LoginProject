using Login.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Client.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int statusCode, Exception exception, bool isAjaxRequet)
        {
            Response.StatusCode = statusCode;

            // If it's not an AJAX request that triggered this action then just retun the view
            if (!isAjaxRequet)
            {
                ErrorModel model = new ErrorModel { HttpStatusCode = statusCode, Exception = exception };

                return View(model);
            }
            else
            {
                // Otherwise, if it was an AJAX request, return an anon type with the message from the exception
                var errorObjet = new { message = exception.Message, data = exception.Data };
                return Json(errorObjet, JsonRequestBehavior.AllowGet);
            }
        }
    }
}