using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace  Salvis.App.Web.Models
{
    [XmlRoot("Item")]
    public class ContentObject
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Content")]
        public string Content { get; set; }
    }
}