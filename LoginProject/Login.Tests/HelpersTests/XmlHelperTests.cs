using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Login.Data.Helpers;
using Login.Models;
using System.Xml;
using Newtonsoft.Json;
using Login.Data.Helpers;

namespace Login.Tests.HelpersTests
{
    /// <summary>
    /// Summary description for XmlHelperTests
    /// </summary>
    [TestClass]
    public class XmlHelperTests
    {
        private string filesPath;

        public XmlHelperTests()
        {
            filesPath = "../../Temp/";

            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            DirectoryInfo directory = new DirectoryInfo(filesPath);
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
        }


        [TestMethod]
        public void CreateDocument_MustCreateXmlFileWithPlurlisedNameOfTheTypeUsedAndNodeElementWithTheSameName()
        {
            XmlStorage storage = new XmlStorage();
            storage.CreateDocument(typeof(ApplicationUser), filesPath);
            string fileName = filesPath + "ApplicationUsers.xml";
            bool fileExist = File.Exists(fileName);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            XmlNode nodeElement = xmlDoc.SelectSingleNode("//ApplicationUsers");

            Assert.IsTrue(fileExist);
            Assert.IsNotNull(nodeElement);
        }

        [TestMethod]
        public void SaveData_WhenSaveUserInEmptyFileTheFirstNodeOfThisFileMustBeEqualToTheUserObject()
        {
            XmlStorage storage = new XmlStorage();
            storage.CreateDocument(typeof(ApplicationUser), filesPath);

            string fileName = filesPath + "ApplicationUsers.xml";
            ApplicationUser user = new ApplicationUser
                 {
                     Id = 1,
                     UserName = "TestName",
                     Password = "TestPass"
                 };
            storage.SaveData<ApplicationUser>(filesPath, user);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            XmlNode nodeElement = xmlDoc.SelectSingleNode("//ApplicationUsers");

            ApplicationUser userNode = JsonConvert.DeserializeObject<ApplicationUser>
                (JsonConvert.SerializeXmlNode(nodeElement.FirstChild, Newtonsoft.Json.Formatting.None, true));

            Assert.IsNotNull(userNode);
            Assert.AreEqual(user.Id, userNode.Id);
            Assert.AreEqual(user.UserName, userNode.UserName);
            Assert.AreEqual(user.Password, userNode.Password);
        }

        [TestMethod]
        public void GetData_IfWeHaveTwoUserNodes_WeMustGetBackTwoUsers()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                                         "<ApplicationUsers>" +
                                            "<ApplicationUser>" +
                                             "<Id>1</Id>" +
                                             "<UserName>User1</UserName>" +
                                             "<Password>Pass1</Password>" +
                                           "</ApplicationUser>" +
                                           "<ApplicationUser>" +
                                             "<Id>2</Id>" +
                                             "<UserName>User2</UserName>" +
                                             "<Password>Pass2</Password>" +
                                           "</ApplicationUser>" +
                                         "</ApplicationUsers>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string fileName = filesPath + "ApplicationUsers.xml";
            doc.Save(fileName);
            XmlStorage storage = new XmlStorage();
            IList<ApplicationUser> users = storage.GetData<ApplicationUser>(filesPath).ToList();

            Assert.AreEqual(2, users.Count());
            Assert.AreEqual(1, users[0].Id);
            Assert.AreEqual(2, users[1].Id);
            Assert.AreEqual("User1", users[0].UserName);
            Assert.AreEqual("User2", users[1].UserName);
        }
    }
}
