using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Login.Data.Helpers
{
    /// <summary>
    /// Class used for saveing to and loading from the cache queryable collection 
    /// of items of arbitrary type <typeparamref name="T"/> representing a type(table) to be stored in the FakeDB
    /// </summary>
    /// <seealso cref="Login.Data.IFakeDbContext">
    internal class XmlStorage
    {
        /// <summary>
        /// Creates a new xml document with name equal to type of <param name="dataType"> and root
        /// note with name equal to type of <param name="dataType">
        /// </summary>
        /// <param name="dataType">The type of the items that will be stored in the document</param>
        /// <param name="path">The path of the document</param>
        internal void CreateDocument(Type dataType, string path)
        {
            string dataName = dataType.Name + "s";
            XmlWriter xmlWriter = XmlWriter.Create(path + dataName + ".xml");

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement(dataName);

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }


        /// <summary>
        /// a queryable collection of arbitrary type <typeparamref name="T"/> from xml file
        /// </summary>
        /// <seealso cref="Login.Data.Helpers.CreateDocument">
        /// <typeparam name="T">The element type of the queryable collection</typeparam>
        /// <param name="path">The path of the xml file</param>
        internal IQueryable<T> GetData<T>(string path) 
        {
            string dataName = typeof(T).Name + "s";
            XDocument xmlDoc = XDocument.Load(path + dataName + ".xml");
            var items =
            from item in xmlDoc.Descendants(typeof(T).Name)
            select JsonConvert.DeserializeObject<T>(JsonConvert.SerializeXNode(item, Newtonsoft.Json.Formatting.None, true));

            return items.AsQueryable();
        }

        /// <summary>
        /// Saves the object of arbitrary type <typeparamref name="T"/> in an already created xml file 
        /// </summary>
        /// <seealso cref="Login.Data.Helpers.CreateDocument">
        /// <typeparam name="T">The element type to be serialized and saved in the file</typeparam>
        /// <param name="path">The path of the xml file</param>
        /// <param name="data">The objets to be serialized and saved in the file</param>
        /// <remarks> 
        /// Kep in mind that only object types that not contain objects of the same type can be serialized
        /// </remarks>
        internal void SaveData<T>(string path, T data)
        {
            XmlDocument xmlDoc = new XmlDocument();

            string dataName = typeof(T).Name + "s";
            string fullFilePath = path + dataName + ".xml";

            xmlDoc.Load(fullFilePath);
            XmlNode nodeElement = xmlDoc.SelectSingleNode("//"+dataName);
            string itemJson = JsonConvert.SerializeObject(data);
            XmlDocument secondXmlDoc = JsonConvert.DeserializeXmlNode(itemJson, typeof(T).Name);
            XmlNode newNode = xmlDoc.ImportNode(secondXmlDoc.FirstChild, true);
            nodeElement.AppendChild(newNode);
            xmlDoc.Save(fullFilePath);
        }
    }
}
