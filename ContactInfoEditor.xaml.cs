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
    public partial class ContactInfoEditor : Window
    {
        public ContactInfo ContactInfo { get; set; }

        public ContactInfoEditor()
        {
            InitializeComponent();
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
            CopyToUi();
        }

        private void CopyToUi()
        {
            Debug.Assert(ContactInfo != null);

            txtTo.Text = ContactInfo.Line1;
            txtAddress1.Text = ContactInfo.Line2;
            txtAddress2.Text = ContactInfo.Line3;
            txtAddress3.Text = ContactInfo.Line4;
            txtEmail.Text = ContactInfo.Email;
            txtPhone.Text = ContactInfo.Phone;
        }

        private void CopyFromUi()
        {
            Debug.Assert(ContactInfo != null);

            ContactInfo.Line1 = txtTo.Text;
            ContactInfo.Line2 = txtAddress1.Text;
            ContactInfo.Line3 = txtAddress2.Text;
            ContactInfo.Line4 = txtAddress3.Text;
            ContactInfo.Email = txtEmail.Text;
            ContactInfo.Phone = txtPhone.Text;
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            if (DialogResult == true)
            {
                CopyFromUi();
            }
        }
    }
}
