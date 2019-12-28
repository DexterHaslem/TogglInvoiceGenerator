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
        public double Rate { get; set; }
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

        public int Dur { get; set; }

        public bool IsBillable { get; set; }

        public double Billable { get; set; }
        public string BillableStr => Billable.ToString("##.00");

        public string Cur { get; set; }

        // dont calculate, we want rounded (eg not return (End - Start).TotalHours)
        public double DurationHrs => new TimeSpan(0, 0, 0, 0, Dur).TotalHours;

        public string TimeStr
        {
            get
            {
                var dateChunk = Start.ToString("d");
                var startTime = Start.ToString("HH:mm");
                var endTime = End.ToString("HH:mm");
                return $"{dateChunk} {startTime} - {endTime}";
            }
        }

        public string StartStr => Start.ToString("d t");
        public string EndStr => End.ToString("d t");
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
