using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Data
{
    /// <summary>
    ///  Representing database implementation
    /// </summary>
    public interface IFakeDbContext
    {

        /// <summary>
        /// Reads all items from the database 
        /// </summary>
        /// <typeparam name="T">The element type of the queryable collection</typeparam>
        /// <returns>А queryable collection of arbitrary type <typeparamref name="T"/> representing table from the datebase implementation</returns>
        IQueryable<T> GetAll<T>();

        /// <summary>
        /// Saves the specified data into the database implementation.
        /// </summary>
        /// <typeparam name="T">The element type of the object to be added in to the datebase implementation.</typeparam>
        /// <param name="data">The object to be added into the database implementation.</param>
        /// <returns>The added item</returns>
        T Save<T>(T data);

        /// <summary> 
        /// Saves the whole collection representing table of objects to the cache
        /// </summary>
        /// <typeparam name="T">The element type of the collection to be added in the cache</typeparam>
        void LoadDataInCache<T>();

    }
}
