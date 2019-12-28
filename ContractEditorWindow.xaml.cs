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
        public ContractEditorWindow()
        {
            InitializeComponent();
        }

        public Contract Contract { get; set; }

        private void FromUi()
        {
            Debug.Assert(Contract != null);
        }

        private void ToUi()
        {
            Debug.Assert(Contract != null);
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            FromUi();
            Close();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
