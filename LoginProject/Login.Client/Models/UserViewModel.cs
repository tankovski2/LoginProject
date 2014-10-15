using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Login.Client.Models
{
    /// <summary>
    ///  Model used for user's sign actions.
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required(ErrorMessage = "The username field is requred")]
        [Display(Name="User Name",Prompt="Enter Username...")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        /// <value>
        /// The password of the user.
        /// </value>
        [Required(ErrorMessage = "The password field is requred")]
        [DataType(DataType.Password)]
        [Display(Prompt="Enter Password...")]
        public string Password { get; set; }
    }
}