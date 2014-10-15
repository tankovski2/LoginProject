using Login.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Login.Client.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class HtmlHelpers
    {

        /// <summary>
        /// Represents the information about an user.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        public static IHtmlString UserInfo(
                this HtmlHelper helper,
                UserDisplayViewModel userModel)
        {
            string result = string.Format("<h2>User: {0}</h2>", userModel.UserName);

            return new HtmlString(result);
        }
    }
}