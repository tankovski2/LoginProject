using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Login.Client.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Gets the storage absolute path.
        /// </summary>
        /// <value>
        /// The storage absolute path.
        /// </value>
        public static string DbFilesAbsolutePath
        {
            get
            {
                string relativeFolderPath = ConfigurationManager.AppSettings["DbsPath"];
                string absoluteFolderPath = AppDomain.CurrentDomain.BaseDirectory + relativeFolderPath;

                return absoluteFolderPath;
            }
        }
    }
}