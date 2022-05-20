using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PharmacyManagementApp.Models
{
	[XmlRoot(ElementName = "DATA")]
	public class DATA
	{

		[XmlElement(ElementName = "NAME")]
		public string NAME { get; set; }

		[XmlElement(ElementName = "PRICE")]
		public double PRICE { get; set; }

		[XmlElement(ElementName = "COUNT")]
		public double COUNT { get; set; }
	}

	[XmlRoot(ElementName = "LS")]
	public class LS
	{

		[XmlElement(ElementName = "DATA")]
		public DATA DATA { get; set; }

		[XmlElement(ElementName = "MNN")]
		public string MNN { get; set; }
	}

	[XmlRoot(ElementName = "EXPORT")]
	public class EXPORT
	{

		[XmlElement(ElementName = "LS")]
		public List<LS> LS { get; set; }
	}
}
