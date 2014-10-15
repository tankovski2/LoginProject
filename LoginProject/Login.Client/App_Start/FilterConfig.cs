using System.Web;
using System.Web.Mvc;

namespace Login.Client
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute
            {
                ExceptionType = typeof(HttpException),
                // Error.cshtml is a view in the Shared folder.
                View = "Error",
                Order = 2
            });
            filters.Add(new HandleErrorAttribute());
        }
    }
}
