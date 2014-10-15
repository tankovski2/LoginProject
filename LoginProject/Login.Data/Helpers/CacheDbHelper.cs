using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Login.Data.Helpers
{
    /// <summary>
    /// Static class used for saveing to and loading from the cache queryable collection of items of of arbitrary type <typeparamref name="T"/> representing a type(table) stored stored in the FakeDB
    /// </summary>
    /// <seealso cref="Login.Data.IFakeDbContext">
    internal static class CacheDbHelper
    {
        /// <summary>
        /// Saves the in cache a queryable collection of arbitrary type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The element type of the queryable collection</typeparam>
        /// <param name="dependencyFilePath">The path to the file whom the cache wil track fo changes.</param>
        /// <param name="items">Collection of items that will be saved in th cache</param>
        internal static void SaveInCache<T>(string dependencyFilePath, IQueryable<T> items)
        {
            string itemName = typeof(T).Name;
            if (HttpRuntime.Cache[itemName] == null)
            {
                HttpRuntime.Cache.Add(itemName, items, new CacheDependency(dependencyFilePath), Cache.NoAbsoluteExpiration,
                    TimeSpan.Zero, CacheItemPriority.Default, null);
            }
        }

        /// <summary>
        /// Loads a queryable collection from the cache.
        /// </summary>
        /// <typeparam name="T">The element type of the queryable collection</typeparam>
        /// <returns>А queryable collection of arbitrary type <typeparamref name="T"/> or null if collection with the name of the <typeparamref name="T"/> is not found in the cache</returns>
        internal static IQueryable<T> LoadFromCache<T>()
        {
            string itemName = typeof(T).Name;

            return HttpRuntime.Cache[itemName] as IQueryable<T>;
        }

    }
}
