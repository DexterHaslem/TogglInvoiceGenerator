using System;
using System.Net.Sockets;
using DevExpress.DataAccess.ObjectBinding;

namespace TogglInvoiceGenerator
{
    internal class ContactInfo
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
        public double HourTotalCost => TotalHoursBilled * HourlyRate;

        public string CustomFooterText1 { get; set; }
        public string CustomFooterText2 { get; set; }

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
