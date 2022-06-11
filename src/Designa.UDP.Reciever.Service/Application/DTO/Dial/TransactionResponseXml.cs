using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Designa.UDP.Reciever.Service.Application.DTO.Dial
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(Response));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (Response)serializer.Deserialize(reader);
	// }


	[XmlRoot(ElementName = "Transaction")]
	public class DialResponseTransaction
	{

		[XmlElement(ElementName = "EntryType")]
		public string EntryType { get; set; }

		[XmlElement(ElementName = "Status")]
		public string Status { get; set; }

		[XmlAttribute(AttributeName = "Number")]
		public string Number { get; set; }

		[XmlElement(ElementName = "ErrorCode")]
		public string ErrorCode { get; set; }

		[XmlElement(ElementName = "ErrorMsg")]
		public string ErrorMsg { get; set; }
	}

	[XmlRoot(ElementName = "Response")]
	public class DialResponse
	{

		[XmlElement(ElementName = "ServicePartnerNo")]
		public string ServicePartnerNo { get; set; }
		[XmlElement(ElementName = "Transaction")]
		public DialResponseTransaction Transaction { get; set; }
	}
}
