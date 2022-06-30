using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Designa.UDP.FTPIntegration
{
    public  class FastagPaymentFtpReport
    {
        public string Location { get; set; }
        public string SubLocation { get; set; }
        public string UserId { get; set; }
        public int WorkShiftNo { get; set; }   
        public string TicketNo { get; set; }
        public string TransType { get; set; }
        public string InDate { get; set; }
        public string InTime { get; set; }
        public string RecipetNo { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentTime { get; set; }
        public double ParkingCharges { get; set; }
        public double Tax { get; set; }
        public double TotalCharges { get; set; }
        public string ExitDate { get; set; }
        public string ExitTime { get; set; }
        public string PaymentType { get; set; }
    }

    public class FastagPaymentFtpReportMapper : ClassMap<FastagPaymentFtpReport>
    {
        public FastagPaymentFtpReportMapper()
        {
            Map(m => m.Location).Index(0).Name("Location");
            Map(m => m.SubLocation).Index(1).Name("Sub Location");
            Map(m => m.UserId).Index(2).Name("User ID");
            Map(m => m.WorkShiftNo).Index(3).Name("WorkShift No");
            Map(m => m.TicketNo).Index(4).Name("ticket no");
            Map(m => m.TransType).Index(5).Name("TransType");
            Map(m => m.InDate).Index(6).Name("In Date");
            Map(m => m.InTime).Index(7).Name("In Time");
            Map(m => m.RecipetNo).Index(8).Name("Receipt No");
            Map(m => m.PaymentDate).Index(9).Name("Payment Date");
            Map(m => m.PaymentTime).Index(10).Name("Payment Time");
            Map(m => m.ParkingCharges).Index(11).Name("Parking Charges");
            Map(m => m.Tax).Index(11).Name("Tax");
            Map(m => m.TotalCharges).Index(12).Name("Total Charges");
            Map(m => m.ExitDate).Index(13).Name("Exit date");
            Map(m => m.ExitTime).Index(14).Name("Exit Time");
            Map(m => m.PaymentType).Index(15).Name("Payment Type");
        }
    }

    public class FastagPaymentFtpExcelHederReportMapper : ClassMap<FastagPaymentFtpReport>
    {
        public FastagPaymentFtpExcelHederReportMapper()
        {
            Map(m => m.Location).Index(0).Default("ABC");

        }
    }
}
