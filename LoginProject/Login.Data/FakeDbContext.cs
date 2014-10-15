using Login.Data.Helpers;
using Login.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Data
{
    /// <summary>
    ///  Representing database using xml files
    /// </summary>
    /// <seealso cref="Login.Data.Helpers.XmlHelper">
    public class FakeDbContext :IFakeDbContext
    {
        /// <summary>
        /// The database xml file path
        /// </summary>
        private string dbFilesPath = "";
        private XmlStorage _storage;

        internal XmlStorage Storage { get { return this._storage; } }


        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDbContext"/> class.
        /// </summary>
        /// <param name="path">The database xml file path</param>
        public FakeDbContext(string path)
        {
            this.dbFilesPath = path;
            _storage = new XmlStorage();
        }
        

        /// <summary>
        /// Reads all items from the database xml file
        /// </summary>
        /// <typeparam name="T">The element type of the queryable collection</typeparam>
        /// <returns>А queryable collection of arbitrary type <typeparamref name="T"/> representing table from the datebase xml file</returns>
        public  IQueryable<T> GetAll<T>()
        {
            CreateDbFileIfNotExist<T>();

            IQueryable<T> items = CacheDbHelper.LoadFromCache<T>();

            if (items==null)
            {
                items = Storage.GetData<T>(dbFilesPath);
                CacheDbHelper.SaveInCache<T>(dbFilesPath, items);
            }

            return items;
        }

        /// <summary>
        /// Saves the specified data into the database xml file.
        /// </summary>
        /// <typeparam name="T">The element type of the object to be added in the datebase xml file</typeparam>
        /// <param name="data">The object to be added into the database xml file</param>
        /// <returns>The added item with included id</returns>
        /// <remarks>
        ///  The <typeparamref name="T"/> should contain public property with name "Id" or "{NameOfTheType}Id"
        /// </remarks>
        public T Save<T>(T data)
        {
            CreateDbFileIfNotExist<T>();

            int allElementsCount = Storage.GetData<T>(dbFilesPath).Count();
            var property = typeof(T).GetProperty("Id");
            if (property==null)
            {
                property = typeof(T).GetProperty(typeof(T).Name + "Id");
            }
            if (property!=null)
            {
                property.SetValue(data, allElementsCount + 1);
            }

            Storage.SaveData<T>(dbFilesPath, data);

            return data;
        }

        /// <summary> 
        /// Saves the whole collection representing table of objects to the cache
        /// </summary>
        /// <typeparam name="T">The element type of the collection to be added in the cache</typeparam>
        public void LoadDataInCache<T>()
        {
            CreateDbFileIfNotExist<T>();
            IQueryable<T> items = Storage.GetData<T>(dbFilesPath);
            CacheDbHelper.SaveInCache<T>(dbFilesPath, items);
        }

        /// <summary>
        /// Creates the database xml file if does not exist.
        /// </summary>
        /// <typeparam name="T">The element type for whom to create a file</typeparam>
        private void CreateDbFileIfNotExist<T>()
        {
            string dbFileName = typeof(T).Name + "s.xml";
            if (!File.Exists(dbFilesPath+dbFileName))
            {
                Storage.CreateDocument(typeof(T), dbFilesPath);
            }
        }
    }
}
