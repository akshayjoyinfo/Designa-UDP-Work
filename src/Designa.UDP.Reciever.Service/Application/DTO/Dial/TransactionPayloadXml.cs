using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Designa.UDP.Reciever.Service.Application.DTO.Dial
{
	[XmlRoot(ElementName = "Item")]
	public class Item
	{

		[XmlElement(ElementName = "ItemCode")]
		public string ItemCode { get; set; }

		[XmlElement(ElementName = "ItemDescription")]
		public string ItemDescription { get; set; }

		[XmlElement(ElementName = "ItemCategory")]
		public string ItemCategory { get; set; }

		[XmlElement(ElementName = "ItemCategoryDescription")]
		public string ItemCategoryDescription { get; set; }

		[XmlElement(ElementName = "ProductGroup")]
		public string ProductGroup { get; set; }

		[XmlElement(ElementName = "ProductGroupDescription")]
		public string ProductGroupDescription { get; set; }

		[XmlElement(ElementName = "BarcodeNo")]
		public string BarcodeNo { get; set; }

		[XmlElement(ElementName = "Quantity")]
		public string Quantity { get; set; }

		[XmlElement(ElementName = "Price")]
		public string Price { get; set; }

		[XmlElement(ElementName = "NetAmount")]
		public string NetAmount { get; set; }

		[XmlElement(ElementName = "PriceInclusiveTax")]
		public string PriceInclusiveTax { get; set; }

		[XmlElement(ElementName = "ChangedPrice")]
		public string ChangedPrice { get; set; }

		[XmlElement(ElementName = "ScaleItem")]
		public string ScaleItem { get; set; }

		[XmlElement(ElementName = "WeighingItem")]
		public string WeighingItem { get; set; }

		[XmlElement(ElementName = "ItemSerialNo")]
		public string ItemSerialNo { get; set; }

		[XmlElement(ElementName = "UOM")]
		public string UOM { get; set; }

		[XmlElement(ElementName = "LineDiscount")]
		public string LineDiscount { get; set; }

		[XmlElement(ElementName = "TotalDiscount")]
		public string TotalDiscount { get; set; }

		[XmlElement(ElementName = "PeriodicDiscount")]
		public string PeriodicDiscount { get; set; }

		[XmlElement(ElementName = "PromotionNo")]
		public string PromotionNo { get; set; }

		[XmlElement(ElementName = "TaxAmount")]
		public string TaxAmount { get; set; }

		[XmlElement(ElementName = "TaxRate")]
		public string TaxRate { get; set; }

		[XmlElement(ElementName = "ServiceTaxAmount")]
		public string ServiceTaxAmount { get; set; }

		[XmlElement(ElementName = "ServiceTaxRate")]
		public string ServiceTaxRate { get; set; }

		[XmlElement(ElementName = "ServiceTaxeCessRate")]
		public string ServiceTaxeCessRate { get; set; }

		[XmlElement(ElementName = "ServiceTaxeCessAmount")]
		public string ServiceTaxeCessAmount { get; set; }

		[XmlElement(ElementName = "ServiceTaxSHECessRate")]
		public string ServiceTaxSHECessRate { get; set; }

		[XmlElement(ElementName = "ServiceTaxSHECessAmount")]
		public string ServiceTaxSHECessAmount { get; set; }
	}

	[XmlRoot(ElementName = "Items")]
	public class Items
	{

		[XmlElement(ElementName = "Item")]
		public Item Item { get; set; }
	}

	[XmlRoot(ElementName = "Payment")]
	public class Payment
	{

		[XmlElement(ElementName = "TenderType")]
		public string TenderType { get; set; }

		[XmlElement(ElementName = "CardNo")]
		public string CardNo { get; set; }

		[XmlElement(ElementName = "CurrencyCode")]
		public string CurrencyCode { get; set; }

		[XmlElement(ElementName = "ExchangeRate")]
		public string ExchangeRate { get; set; }

		[XmlElement(ElementName = "AmountTendered")]
		public string AmountTendered { get; set; }

		[XmlElement(ElementName = "AmountInCurrency")]
		public string AmountInCurrency { get; set; }
	}

	[XmlRoot(ElementName = "Payments")]
	public class Payments
	{

		[XmlElement(ElementName = "Payment")]
		public Payment Payment { get; set; }
	}

	[XmlRoot(ElementName = "Transaction")]
	public class Transaction
	{

		[XmlElement(ElementName = "TransactionNo")]
		public string TransactionNo { get; set; }

		[XmlElement(ElementName = "OriginalRefNo")]
		public string OriginalRefNo { get; set; }

		[XmlElement(ElementName = "EntryType")]
		public string EntryType { get; set; }

		[XmlElement(ElementName = "StoreNo")]
		public string StoreNo { get; set; }

		[XmlElement(ElementName = "POSNo")]
		public string POSNo { get; set; }

		[XmlElement(ElementName = "StaffID")]
		public string StaffID { get; set; }

		[XmlElement(ElementName = "StaffName")]
		public string StaffName { get; set; }

		[XmlElement(ElementName = "TransactionDate")]
		public string TransactionDate { get; set; }

		[XmlElement(ElementName = "TransactionTime")]
		public string TransactionTime { get; set; }

		[XmlElement(ElementName = "DiscountAmount")]
		public string DiscountAmount { get; set; }

		[XmlElement(ElementName = "TotalDiscount")]
		public string TotalDiscount { get; set; }

		[XmlElement(ElementName = "TableNo")]
		public string TableNo { get; set; }

		[XmlElement(ElementName = "NoOfCovers")]
		public string NoOfCovers { get; set; }

		[XmlElement(ElementName = "CustomerName")]
		public string CustomerName { get; set; }

		[XmlElement(ElementName = "Address")]
		public string Address { get; set; }

		[XmlElement(ElementName = "Gender")]
		public string Gender { get; set; }

		[XmlElement(ElementName = "PassportNo")]
		public string PassportNo { get; set; }

		[XmlElement(ElementName = "Nationality")]
		public string Nationality { get; set; }

		[XmlElement(ElementName = "PortofBoarding")]
		public string PortofBoarding { get; set; }

		[XmlElement(ElementName = "PortofDisembarkation")]
		public string PortofDisembarkation { get; set; }

		[XmlElement(ElementName = "FlightNo")]
		public string FlightNo { get; set; }

		[XmlElement(ElementName = "SectorNo")]
		public string SectorNo { get; set; }

		[XmlElement(ElementName = "BoardingPassNo")]
		public string BoardingPassNo { get; set; }

		[XmlElement(ElementName = "SeatNo")]
		public string SeatNo { get; set; }

		[XmlElement(ElementName = "Airlines")]
		public string Airlines { get; set; }

		[XmlElement(ElementName = "ServiceChargeAmount")]
		public string ServiceChargeAmount { get; set; }

		[XmlElement(ElementName = "NetTransactionAmount")]
		public string NetTransactionAmount { get; set; }

		[XmlElement(ElementName = "GrossTransactionAmount")]
		public string GrossTransactionAmount { get; set; }

		[XmlElement(ElementName = "SequenceNumber")]
		public string SequenceNumber { get; set; }

		[XmlElement(ElementName = "Items")]
		public Items Items { get; set; }

		[XmlElement(ElementName = "Payments")]
		public Payments Payments { get; set; }
	}

	[XmlRoot(ElementName = "Transactions")]
	public class Transactions
	{

		[XmlElement(ElementName = "Transaction")]
		public Transaction Transaction { get; set; }
	}

	[XmlRoot(ElementName = "TransactionData")]
	public class TransactionData
	{

		[XmlElement(ElementName = "ServicePartnerNo")]
		public string ServicePartnerNo { get; set; }

		[XmlElement(ElementName = "Password")]
		public string Password { get; set; }

		[XmlElement(ElementName = "Transactions")]
		public Transactions Transactions { get; set; }

		[XmlAttribute(AttributeName = "noNamespaceSchemaLocation", Namespace = XmlSchema.InstanceNamespace)]
		public string noNamespaceSchemaLocation = "http://10.248.16.35/_URL_/Dap T2 Parking.xsd";

		[XmlAttribute(Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
		public string Xsi { get; set; }

	}
}
