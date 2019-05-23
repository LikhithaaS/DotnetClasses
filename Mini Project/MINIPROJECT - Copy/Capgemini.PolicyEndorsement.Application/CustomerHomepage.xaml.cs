using System;
using System.Collections.Generic;
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

namespace Capgemini.PolicyEndorsement.Application
{
    /// <summary>
    /// Interaction logic for CustomerHomepage.xaml
    /// </summary>
    public partial class CustomerHomepage : Window
    {
        public CustomerHomepage()
        {
            InitializeComponent();
        }

        private void TxtSearchandUpdate_Click(object sender, RoutedEventArgs e)
        {
            SearchPolicy obj = new SearchPolicy();
            obj.Show();
        }

        private void TxtViewEndorse_Click(object sender, RoutedEventArgs e)
        {
            EndorsementPage obj1 = new EndorsementPage();
            obj1.txtCnum.Text = txtCustNum.Text;
            obj1.Show();
        }

        private void TxtStatus_Click(object sender, RoutedEventArgs e)
        {
            TransactionDetailsID obj3 = new TransactionDetailsID();
            obj3.txtcustNum.Text = txtCustNum.Text;
            obj3.Show();
        }
    }
}
