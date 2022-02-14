using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Designa.UDP.Reciever.Service.Application.DTO
{
    public partial class FastagEntryMessage
    {
        [JsonProperty("FastagEntry")]
        public FastagEntry FastagEntry { get; set; }

        [JsonProperty("FastagEntryRes")]
        public FastagEntryRes FastagEntryRes { get; set; }
    }

    public class FastagEntry
    {
        [JsonProperty("entry_lane_id")]
        public string EntryLaneId { get; set; }

        [JsonProperty("entry_tags")]
        public List<EntryTag> EntryTags { get; set; }

        [JsonProperty("entry_time")]
        public long? EntryTime { get; set; }

        [JsonProperty("lp_number")]
        public string LpNumber { get; set; }

        [JsonProperty("plaza_id")]
        public string PlazaId { get; set; }

        [JsonProperty("ticket_id")]
        public string TicketId { get; set; }
    }

    public partial class EntryTag
    {
        [JsonProperty("epc_id")]
        public string EpcId { get; set; }

        [JsonProperty("t_id")]
        public string TId { get; set; }

        [JsonProperty("user_memory")]
        public string UserMemory { get; set; }
    }

    public partial class FastagEntryRes
    {
        [JsonProperty("details")]
        public List<Detail> Details { get; set; }

        [JsonProperty("response_id")]
        public long ResponseId { get; set; }

        [JsonProperty("response_timestamp")]
        public string ResponseTimestamp { get; set; }

        [JsonProperty("ticket_id")]
        public string TicketId { get; set; }
    }

    public partial class Detail
    {
        [JsonProperty("epc_id")]
        public string EpcId { get; set; }

        [JsonProperty("success_flag")]
        public bool SuccessFlag { get; set; }

        [JsonProperty("status_code")]
        public long StatusCode { get; set; }

        [JsonProperty("status_desc")]
        public string StatusDesc { get; set; }

        [JsonProperty("vehicle_number")]
        public string VehicleNumber { get; set; }

        [JsonProperty("chassis_number")]
        public string ChassisNumber { get; set; }
    }

}
