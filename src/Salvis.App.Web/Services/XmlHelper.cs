using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Salvis.App.Web.Services
{
    public class XmlHelper<T>
    {
        private readonly XDocument _xmlDocument;

        public XmlHelper(string xmlPath)
        {
            if (string.IsNullOrWhiteSpace(xmlPath)) throw new ArgumentNullException("xmlPath");

            xmlPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, xmlPath);
            
            _xmlDocument = XDocument.Load(xmlPath);
        }

        /// <summary>
        /// Get FirstOrDefault that meets the attributeConditions.
        /// </summary>
        /// <param name="attributeConditions">Comparison condiction where key is the attribute and value is the value.</param>
        /// <returns></returns>
        public T Get(Dictionary<string, string> attributeConditions)
        {
            if (_xmlDocument == null) throw new NullReferenceException("XDocument is not initialized.");
            if (_xmlDocument.Root == null) throw new NullReferenceException("No tiene el elemento Root.");


            var elements =
                _xmlDocument.Root.Elements().FirstOrDefault(
                                p =>
                                attributeConditions.All(
                                    x =>
                                    p.Attribute(x.Key)
                                     .Value.Equals(x.Value, StringComparison.InvariantCultureIgnoreCase)));

            return ToEntity<T>(elements);

        }

        /// <summary>
        /// Convert an XElement to entity.
        /// </summary>
        /// <param name="element">Element to Convert.</param>
        /// <returns></returns>
        public static TX ToEntity<TX>(XElement element)
        {
            var xmlSerializer = new XmlSerializer(typeof(TX));
            var entity = (TX)xmlSerializer.Deserialize(element.CreateReader());
            return entity;
        }
    }
}