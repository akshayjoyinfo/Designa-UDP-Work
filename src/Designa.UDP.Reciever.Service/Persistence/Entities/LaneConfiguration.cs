using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Designa.UDP.Reciever.Service.Persistence.Entities
{
    [Table("lane_configurations")]
    public class LaneConfiguration
    {

        [Key]
        public long Id { get; set; }
        public string LaneId { get; set; }
        public string Ip { get; set; }
        public string DisplayResetMessage { get; set; }
        public DateTime? LastModifiedUtc { get; set; }
    }
}
