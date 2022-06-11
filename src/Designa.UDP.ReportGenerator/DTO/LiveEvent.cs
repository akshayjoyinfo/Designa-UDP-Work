using System;
using System.Collections.Generic;
using System.Text;

namespace Designa.UDP.ReportGenerator.DTO
{
    public class EventLog
    {
        public DateTime? EventDate { get; set; }
        public string LaneId { get; set; }
        public string FastagEvent { get; set; }
    }
}
