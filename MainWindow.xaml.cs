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
        private ViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new ViewModel(this);
        }

        private void GenerateReport(object sender, RoutedEventArgs e)
        {
            viewModel.GenerateReport(e);
        }

        private void OnEditContactInformation(object sender, RoutedEventArgs e)
        {
            viewModel.OnEditContactInformation(e);
        }

        private void OnAddEditContract(object sender, RoutedEventArgs e)
        {
            viewModel.OnAddEditContract(e);
        }

        private void OnDupContract(object sender, RoutedEventArgs e)
        {
            viewModel.OnDuplicateContract(e);
        }

        private void OnDeleteContract(object sender, RoutedEventArgs e)
        {
            viewModel.OnDeleteContract(e);
        }
    }
}
