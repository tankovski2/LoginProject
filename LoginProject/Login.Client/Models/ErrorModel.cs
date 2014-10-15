using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Client.Models
{
    /// <summary>
    /// Representing the HTTP response
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        /// <value>
        /// The HTTP status code.
        /// </value>
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the exception caused the eror.
        /// </summary>
        /// <value>
        /// The exception caused the eror.
        /// </value>
        public Exception Exception { get; set; }
    }
}