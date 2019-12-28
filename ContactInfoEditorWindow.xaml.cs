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
    /// Interaction logic for ContactInfoEditor.xaml
    /// </summary>
    public partial class ContactInfoEditorWindow : Window
    {
        public ContactInfo ContactInfo { get; set; }

        public ContactInfoEditorWindow()
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
            this.DialogResult = false;
            Close();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            if (DialogResult == true)
            {
            }
        }
    }
}
