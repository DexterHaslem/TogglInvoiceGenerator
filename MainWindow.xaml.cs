using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraPrinting;

namespace TogglInvoiceGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel(this);
        }

        private void PreviewTest(object sender, RoutedEventArgs e)
        {
            /* pdf */
            //const string testFile = "invoice.pdf";
            //PdfExportOptions options = new PdfExportOptions();
            //options.PdfACompatibility = PdfACompatibility.PdfA1b;
            

            /* docx */
            const string testFile = "invoice.docx";
            var docxExportOptions = new DocxExportOptions();
            docxExportOptions.AllowFloatingPictures = true;
            docxExportOptions.RasterizeImages = false;
            docxExportOptions.RasterizationResolution = 96;
            docxExportOptions.KeepRowHeight = true;

            if (File.Exists(testFile))
            {
                File.Delete(testFile);
            }
            var report = new Report();
            ((ObjectDataSource)report.DataSource).DataSource = new ReportDataSource();
            report.CreateDocument();

            //report.ExportToPdf(testFile, options);
            report.ExportToDocx(testFile, docxExportOptions);
            Process.Start(testFile);
        }
    }
}
