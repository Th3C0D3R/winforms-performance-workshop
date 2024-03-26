using System.Collections.Generic;
using System.Xml.Serialization;

namespace PerformanceDemoApp.Entities
{
    [XmlRoot("response")]
    internal class Response
    {
        [XmlElement("row")]
        public List<Row> Rows { get; set; } = new List<Row>();
    }
}
