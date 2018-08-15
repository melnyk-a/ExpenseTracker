using ExpenseTracker.Models.BasicIdentities;
using ExpenseTracker.Models.Databases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace ExpenseTracker.Models.XmlDocuments
{
    internal sealed class XmlDataBaseDocument<T>
    {
        private readonly string constructorAttributeName = "Name";
        private readonly IDatabaseProvider<T> databaseProvider;
        private readonly string fileName;
        private readonly string genericName;
        private readonly Type genericType;
        private readonly Type type;

        public XmlDataBaseDocument(IDatabaseProvider<T> dataBaseProvider)
        {
            this.databaseProvider = dataBaseProvider;
            type = dataBaseProvider.GetType();
            genericType = type.GenericTypeArguments[0];
            genericName = genericType.Name;
            fileName = $"{type.Name.Remove(type.Name.Length - 2)}{genericName}.xml";

            dataBaseProvider.DatabaseChanged += (sender, e) =>
              {
                  Save();
              };
        }

        public void Load()
        {
            if (File.Exists(fileName))
            {
                var accountsToLoad = new List<T>();
                var document = new XmlDocument();

                document.Load(fileName);
                XmlNode accounts = document.DocumentElement;
                foreach (XmlNode account in accounts)
                {
                    var AccountToLoad = Activator.CreateInstance(genericType, new[] { account.Attributes[constructorAttributeName].Value });
                    PropertyInfo[] properties = genericType.GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.CanWrite)
                        {
                            if (property.PropertyType.Equals(typeof(int)))
                            {
                                int value = Convert.ToInt32(account.Attributes[property.Name].Value);
                                property.SetValue(AccountToLoad, value);
                            }
                            else if (property.PropertyType.Equals(typeof(DateTime)))
                            {
                                DateTime value = Convert.ToDateTime(account.Attributes[property.Name].Value);
                                property.SetValue(AccountToLoad, value);
                            }
                            else
                            {
                                property.SetValue(AccountToLoad, account.Attributes[property.Name].Value);
                            }
                        }
                    }
                    accountsToLoad.Add((T)AccountToLoad);
                }

                foreach (var item in accountsToLoad)
                {
                    databaseProvider.AddItem(item);
                }
            }
        }

        public void Save()
        {
            var document = new XmlDocument();

            XmlElement collection = document.CreateElement("Collection");

            PropertyInfo[] properties = genericType.GetProperties();
            foreach (var accountSave in databaseProvider.Items)
            {
                XmlElement account = document.CreateElement(genericName);
                foreach (PropertyInfo property in properties)
                {
                    XmlAttribute attribute;
                    if (property.Name.Equals(constructorAttributeName))
                    {
                        attribute = document.CreateAttribute(constructorAttributeName);
                    }
                    else
                    {
                        attribute = document.CreateAttribute(property.Name);
                    }
                    attribute.Value = property.GetValue(accountSave).ToString();
                    account.Attributes.Append(attribute);
                }
                collection.AppendChild(account);
            }

            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-8", "no");
            document.AppendChild(declaration);
            document.AppendChild(collection);
            document.Save(fileName);
        }
    }
}