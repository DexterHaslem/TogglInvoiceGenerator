using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Xml.Serialization;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Office.NumberConverters;
using TogglAPI.Models;

namespace TogglInvoiceGenerator
{
    [Serializable]
    public class Persistence
    {
        private const string SaveFile = "tinvoicegen.xml";

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

            using (var fs = File.OpenRead(SaveFile))
            {
                var serializer = new XmlSerializer(typeof(Persistence));
                var restored = (Persistence)serializer.Deserialize(fs);
                return restored;
            }
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(Persistence));
            using (var fs = File.OpenWrite(SaveFile))
            {
                serializer.Serialize(fs, this);
            }
        }
    }

    [Serializable]
    public class Contract
    { 
        public string PONum { get; set; }
        public DateTime PeriodOfPerformanceStart { get; set; }
        public DateTime PeriodOfPerformanceEnd { get; set; }
        public ContactInfo AccountsPayable { get; set; }
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
    }

    [HighlightedClass]
    internal class ReportDataSource
    {
        public ContactInfo From { get; set; }
        public ContactInfo To { get; set; }
        
        public string InvoiceNum { get; set; }
        public string InvoiceDate { get; set; }
        public string ContractPONum { get; set; }
        public string VendorNum { get; set; }

        public string PeriodOfPerformance { get; set; }
        public string ContractPeriodOfPerformance { get; set; }

        public double TotalHoursBilled { get; set; }
        public double HourlyRate { get; set; }
        public double HourTotalCost { get; set; }// => TotalHoursBilled * HourlyRate;

        public string CustomFooterText1 { get; set; }
        public string CustomFooterText2 { get; set; }

        public ReportTimeEntry[] TimeEntries { get; set; }

        public ReportDataSource(Contract selectedContract, Project project, DetailReportResponse detailReport) : this()
        {
            HourTotalCost = detailReport.TotalCurrencies[0].Amount;
            HourlyRate = project.Rate;
            TimeEntries = detailReport.Data;
        }

        public ReportDataSource()
        {
            From = new ContactInfo
            {
                Email = "foo@barf.com",
                Phone = "970-633-0324",
                Line1 = "Line 1",
                Line2 = "Line 2",
                Line3 = "Line 3",
                Line4 = "Line 4",
            };

            To = new ContactInfo
            {
                Email = "to@foobar.com",
                Phone = "123445124111",
                Line1 = "Line 1",
                Line2 = "Line 2",
                Line3 = "Line 3",
                Line4 = "Line 4",
            };

            VendorNum = "HAS007";
            InvoiceDate = DateTime.Now.Date.ToShortDateString();
            ContractPONum = "PO1234";
            PeriodOfPerformance = "12/1/ - 12/31/2019";
            ContractPeriodOfPerformance = "1/1/2019 - 12/31/2019";
            CustomFooterText1 = "Footer text 1";
            CustomFooterText2 = "Footer text 2";
            InvoiceNum = "1004";
        }
    }
}
