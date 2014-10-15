using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Client.Models
{
    /// <summary>
    /// Model representing application user
    /// </summary>
    /// <seealso cref="Login.Models.ApplicationUser"/>
    public class UserDisplayViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}