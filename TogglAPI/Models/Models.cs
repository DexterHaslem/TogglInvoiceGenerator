using System;
using Newtonsoft.Json;

namespace TogglAPI.Models
{
    public class Workspace
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Rounding { get; set; }
        public int RoundingMinutes { get; set; }
    }

    public class Project
    {
        public long Id { get; set; }
        public long Wid { get; set; }
        public long Cid { get; set; }
        public string Name { get; set; }
        public bool Billable { get; set; }
        public bool Active { get; set; }
    }

    public class ReportTimeEntry
    {
        public long Id { get; set; }
        public long Pid { get; set; }
        public string Project { get; set; }
        public string Client { get; set; }
        // public long Tid { get; set; }
        // public long Uid { get; set; }
        public string User { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public long Dur { get; set; }

        public bool IsBillable { get; set; }

        public long Billable { get; set; }
        public string Cur { get; set; }
    }

    public class CurrencyAmount
    {
        public string Currency { get; set; }
        public double Amount { get; set; }
    }
    public class DetailReportResponse
    {
        [JsonProperty("total_grand")]
        public long TotalGrand { get; set; }

        [JsonProperty("total_billable")]
        public long TotalBillable { get; set; }

        [JsonProperty("total_currencies")]
        public CurrencyAmount[] TotalCurrencies { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        public int PerPage { get; set; }
        public ReportTimeEntry[] Data { get; set; }
    }
}
