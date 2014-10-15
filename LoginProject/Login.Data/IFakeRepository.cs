using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Data
{
    /// <summary>
    /// A screen for using implementation of storage context
    /// </summary>
    /// <typeparam name="T">The element type of the objects that the repository will store</typeparam>
    /// <remarks>
    /// The type <typeparamref name="T"/> should be an instance of a class.
    /// </remarks>
    public interface IFakeRepository<T> where T : class
    {

        /// <summary>
        /// Makes a query for all items in the context.
        /// </summary>
        /// <returns>A queryable collection of items from the storage context</returns>
        IQueryable<T> All();

        /// <summary>
        /// Saves the specified model into the storage context.
        /// </summary>
        /// <param name="model">The model to be saved into the storage context.</param>
        /// <returns>The saved model</returns>
        T Save(T model);

        /// <summary> 
        /// Loads data from storage the context in the cache
        /// </summary>
        void LoadDataInCache();
    }
}
