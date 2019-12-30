using System;
using System.IO;
using System.Xml.Serialization;
using DevExpress.DataAccess.ObjectBinding;
using TogglAPI.Models;

namespace TogglInvoiceGenerator
{
    [Serializable]
    public class Persistence
    {
        private const string SaveFile = "tinvoicegen.xml";

        public string CompanyName { get; set; }
        public string VendorNo { get; set; }
        public string PoP { get; set; }
        public int InvoiceNo { get; set; }

        public Contract[] Contracts { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public Persistence()
        {
            Contracts = new Contract[0];
            ContactInfo = new ContactInfo();
        }

        public static Persistence Restore()
        {
            if (!File.Exists(SaveFile))
            {
                return new Persistence();
            }

            try
            {
                using (var fs = File.OpenRead(SaveFile))
                {
                    var serializer = new XmlSerializer(typeof(Persistence));
                    var restored = (Persistence) serializer.Deserialize(fs);
                    return restored;
                }
            }
            catch (InvalidOperationException)
            {
                return new Persistence();
            }
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(Persistence));
            using (var fs = File.Open(SaveFile, FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }
    }

    [Serializable]
    public class Contract
    { 
        public string DisplayName { get; set; }
        public string PONum { get; set; }
        public string CSA { get; set; }
        public string PeriodOfPerformance { get; set; }
        public ContactInfo Contact { get; set; }

        public Contract()
        {
            DisplayName = "";
            PONum = "";
            CSA = "";
            PeriodOfPerformance = "";
            Contact = new ContactInfo();
        }

        public Contract(Contract other) : this()
        {
            if (other == null)
                return;
            DisplayName = other.DisplayName;
            PONum = other.PONum;
            CSA = other.CSA;
            PeriodOfPerformance = other.PeriodOfPerformance;
            Contact = new ContactInfo(other.Contact);
        }
    }

    [Serializable]
    public class ContactInfo
    {
        // dont try to be clever here, we just want simple bunches of lines
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public ContactInfo()
        {
        }

        public ContactInfo(ContactInfo other)
        {
            if (other == null)
                return;
            Line1 = other.Line1;
            Line2 = other.Line2;
            Line3 = other.Line3;
            Line4 = other.Line4;
            Email = other.Email;
            Phone = other.Phone;
        }
    }

    [HighlightedClass]
    internal class ReportDataSource
    {
        public ContactInfo From { get; set; }
        public string InvoiceNum { get; set; }
        public string InvoiceDate { get; set; }
        public string VendorNum { get; set; }

        public string CompanyName { get; set; }

        public Contract Contract { get; set; }

        public string PeriodOfPerformance { get; set; }

        public double TotalHoursBilled { get; set; }
        public double HourlyRate { get; set; }
        public double HourTotalCost { get; set; }// => TotalHoursBilled * HourlyRate;

        public string CustomFooterText1 { get; set; }
        public string CustomFooterText2 { get; set; }

        public ReportTimeEntry[] TimeEntries { get; set; }

        public ReportDataSource(Contract contract, Project project, DetailReportResponse detailReport) : this()
        {
            Contract = contract;
            TotalHoursBilled = detailReport.TotalBillable;
            HourTotalCost = detailReport.TotalCurrencies[0].Amount;
            HourlyRate = project.Rate;
            TimeEntries = detailReport.Data;
        }

        public ReportDataSource()
        {
            InvoiceDate = DateTime.Now.Date.ToShortDateString();
        }
    }
}
