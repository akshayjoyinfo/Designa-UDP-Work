using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Designa.UDP.Reciever.Service.Persistence.Entities
{
    [Table("fastag_entries")]
    public class FastagEntryEntity
    {
        [Key]
        public long Id { get; set; }
        public string TicketId { get; set; }
        public string EntryLaneId { get; set; }
        public long? EntryTime { get; set; }
        public DateTime? EntryTimeConverted { get; set; }
        public string EntryResponseStatusCode { get; set; }
        public string EntryResponseStatusDesc { get; set; }
        public string VehicleNumber { get; set; }
        public long? EntryResponseTime { get; set; }
        public DateTime? EntryResponseTimeConverted { get; set; }

        public string EntryUdpMessage { get; set; }

        public DateTime LastModifiedUtc { get; set; }
    }
}
