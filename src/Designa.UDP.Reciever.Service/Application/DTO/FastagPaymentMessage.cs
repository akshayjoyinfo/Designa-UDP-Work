using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Designa.UDP.Reciever.Service.Application.DTO
{
    public class FastagPaymentMessage
    {
        [JsonProperty("FastagPayment")]
        public FastagPayment FastagPayment { get; set; }

        [JsonProperty("FastagPaymentRes")]
        public FastagPaymentRes FastagPaymentRes { get; set; }
    }

    public partial class FastagPayment
    {
        [JsonProperty("entry_lane_id")]
        public string EntryLaneId { get; set; }

        [JsonProperty("entry_time")]
        public long? EntryTime { get; set; }

        [JsonProperty("epc_id")]
        public string EpcId { get; set; }

        [JsonProperty("exit_lane_id")]
        
        public string ExitLaneId { get; set; }

        [JsonProperty("exit_time")]
        public long ExitTime { get; set; }

        [JsonProperty("lp_number")]
        public string LpNumber { get; set; }

        [JsonProperty("plaza_id")]
        public string PlazaId { get; set; }

        [JsonProperty("t_id")]
        public string TId { get; set; }

        [JsonProperty("ticket_id")]
        public string TicketId { get; set; }

        [JsonProperty("txn_amount")]
        public decimal TxnAmount { get; set; }

        [JsonProperty("user_memory")]
        public string UserMemory { get; set; }
    }

    public partial class FastagPaymentRes
    {
        [JsonProperty("response_id")]
        public long ResponseId { get; set; }

        [JsonProperty("txn_ack_flag")]
        public bool TxnAckFlag { get; set; }

        [JsonProperty("status_code")]
        public long? StatusCode { get; set; }

        [JsonProperty("status_desc")]
        public string StatusDesc { get; set; }

        [JsonProperty("response_timestamp")]
        public string ResponseTimestamp { get; set; }

        [JsonProperty("npci_transaction_id")]
        public string NpciTransactionId { get; set; }
    }
}
