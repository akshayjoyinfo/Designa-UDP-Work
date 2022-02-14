using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Designa.UDP.Reciever.Service.Persistence.Entities
{
    [Table("fastag_payments")]
    public class FastagPaymentEntity
    {
        [Key]
        public long Id { get; set; }
        public string TicketId { get; set; }
        public string EntryLaneId { get; set; }
        public string ExitLaneId { get; set; }
        public long? EntryTime { get; set; }
        public DateTime? EntryTimeConverted { get; set; }
        public long? ExitTime { get; set; }
        public DateTime? ExitTimeConverted { get; set; }
        public string NpciTransactionId { get; set; }
        public string EpcId { get; set; }
        public decimal TxnAmount { get; set; }

        public string FastagResponseStatusCode { get; set; }
        public string FastagResponseStatusDesc { get; set; }
        public string PaymentUdpMessage { get; set; }
        public DateTime? LastModifiedUtc { get; set; }
    }
}
