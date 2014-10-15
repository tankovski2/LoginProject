using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Data
{
    /// <summary>
    /// A screen for using <see cref="Login.Data.IFakeDbContext"/>
    /// </summary>
    /// <typeparam name="T">The element type of the objects that the repository will store</typeparam>
    /// <remarks>
    /// The type <typeparamref name="T"/> should be an instance of a class.
    /// </remarks>
    public class FakeRepository<T> : IFakeRepository<T> where T : class
    {
        /// <summary>
        /// Instance of the database implementation context
        /// </summary>
        private IFakeDbContext data;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeRepository{T}"/> class.
        /// </summary>
        /// <param name="db">The database implementation context.</param>
        public FakeRepository(IFakeDbContext db)
        {
            data = db;
        }

        /// <summary>
        /// Makes a query for all items in the context.
        /// </summary>
        /// <returns>A queryable collection of items from the database implementation context</returns>
        public IQueryable<T> All()
        {
            return data.GetAll<T>();
        }

        /// <summary>
        /// Saves the specified model into the database implementation context.
        /// </summary>
        /// <param name="model">The model to be saved into the database implementation context.</param>
        /// <returns>The saved model</returns>
        public T Save(T model)
        {
            return data.Save<T>(model);
        }

        /// <summary> 
        /// Uses a build in method in the context for caching tha data
        /// </summary>
        /// <seealso cref="Login.Data.IFakeDbContext.LoadDataInCache<T>">
        public void LoadDataInCache()
        {
            data.LoadDataInCache<T>();
        }
    }
}
