using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Designa.UDP.Reciever.Service.Persistence.Entities
{
    [Table("fastag_audit_report")]
    public class FastagAuditReport
    {
        [Key]
        public long Id { get; set; }
        public string ParkingNode { get; set; }
        public string TransactionId { get; set; }
        public string TicketId { get; set; }
        public string CardTagNo { get; set; }
        public DateTime? DateTimeOfEntry { get; set; }
        public DateTime? DateTimeOfExit { get; set; }
        public string VehicleNumber { get; set; }
        public string EntryDeviceName { get; set; }
        public string ExitDeviceName { get; set; }
        public string ModeOfPayment { get; set; }
        public decimal ParkingFeeGross { get; set; }
        public decimal Tax { get; set; }
        public decimal ParkingFeeNet { get; set; }
        public string StayDuration { get; set; }
        public DateTime? LastModifiedUtc { get; set; }

    }
}
