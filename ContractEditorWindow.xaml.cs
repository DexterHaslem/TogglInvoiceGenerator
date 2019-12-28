using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace TogglInvoiceGenerator
{
    /// <summary>
    /// Interaction logic for ContractEditorWindow.xaml
    /// </summary>
    public partial class ContractEditorWindow : Window
    {
        public Contract Contract { get; set; }

        public ContractEditorWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
