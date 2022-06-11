using CsvHelper.Configuration;
using Designa.UDP.Reciever.Service.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Designa.UDP.ReportGenerator.DTO
{
    public class AuditReportExcelMapper : ClassMap<FastagAuditReport>
    {
        public AuditReportExcelMapper()
        {
            Map(m => m.ParkingNode).Index(0).Name("PARKING NODE");
            Map(m => m.TransactionId).Index(1).Name("TRANSACTION ID")
                .Convert(row=> "_"+row.Value.TransactionId+"_");
            Map(m => m.TicketId).Index(2).Name("TICKET ID");
            Map(m => m.CardTagNo).Index(3).Name("CARD TAG NO");
            Map(m => m.DateTimeOfEntry).Index(4).Name("DATE TIME OF ENTRY");
            Map(m => m.VehicleNumber).Index(5).Name("VEHICLE NUMBER");
            Map(m => m.EntryDeviceName).Index(6).Name("ENTRY DEVICE NAME");
            Map(m => m.DateTimeOfExit).Index(7).Name("DATE TIME OF EXIT");
            Map(m => m.ExitDeviceName).Index(8).Name("EXIT DEVICE NAME");
            Map(m => m.ModeOfPayment).Index(9).Name("MODE OF PAYMENT");
            Map(m => m.ParkingFeeGross).Index(10).Name("PARKING FEE GROSS");
            Map(m => m.Tax).Index(11).Name("TAX");
            Map(m => m.ParkingFeeNet).Index(12).Name("PARKING FEE NET");
            Map(m => m.StayDuration).Index(13).Name("STAY DURATION");
        }
    }

    public class AuditReportExcelFooterMapper : ClassMap<FastagAuditReport>
    {
        public AuditReportExcelFooterMapper()
        {
            Map(m => m.ParkingNode).Index(0).Name("PARKING NODE");
            Map(m => m.TransactionId).Index(1).Name("TRANSACTION ID");
            Map(m => m.TicketId).Index(2).Name("TICKET ID");
            Map(m => m.CardTagNo).Index(3).Name("CARD TAG NO");
            Map(m => m.DateTimeOfEntry).Index(4).Name("DATE TIME OF ENTRY");
            Map(m => m.VehicleNumber).Index(5).Name("VEHICLE NUMBER");
            Map(m => m.EntryDeviceName).Index(6).Name("ENTRY DEVICE NAME");
            Map(m => m.DateTimeOfExit).Index(7).Name("DATE TIME OF EXIT");
            Map(m => m.ExitDeviceName).Index(8).Default("Total");
            Map(m => m.ModeOfPayment).Index(9);
            Map(m => m.ParkingFeeGross).Index(10);
            Map(m => m.Tax).Index(11);
            Map(m => m.ParkingFeeNet).Index(12);
            
        }
    }

    public class AuditReportExcelHeaderMapper : ClassMap<FastagAuditReport>
    {
        public AuditReportExcelHeaderMapper()
        {
            Map(m => m.ParkingNode).Index(0);
            Map(m => m.TransactionId).Index(1);
            Map(m => m.TicketId).Index(2).Default("Generated On:");
            Map(m => m.CardTagNo).Index(3);

        }
    }

    public class AuditReportHeaderMapper : ClassMap<FastagAuditReport>
    {
        public AuditReportHeaderMapper()
        {
            Map(m => m.ParkingNode).Index(0);
            Map(m => m.TransactionId).Index(1);
            Map(m => m.TicketId).Index(2).Default("Generated On:");
            Map(m => m.CardTagNo).Index(3);
            Map(m => m.DateTimeOfEntry).Index(4);
            Map(m => m.VehicleNumber).Index(5);
            Map(m => m.EntryDeviceName).Index(6);

        }
    }
}
