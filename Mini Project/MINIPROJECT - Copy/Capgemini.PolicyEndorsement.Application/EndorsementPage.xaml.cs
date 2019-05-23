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
using System.Data;
using System.Data.SqlClient;
using Capgemini.PolicyEndorsement.BusinessLayer;

namespace Capgemini.PolicyEndorsement.Application
{
    /// <summary>
    /// Interaction logic for EndorsementPage.xaml
    /// </summary>
    public partial class EndorsementPage : Window
    {
        public EndorsementPage()
        {
            InitializeComponent();
        }
        public void ShowEndorsement()
        {
            string custnum = txtCnum.Text;
            DataTable dt = new DataTable();
            dt = PolicyBL.GetAllEndorsementCustBL(custnum);
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show($"No Endorsement Records generated for {custnum}");
            }
            else
            {
                dgendorsement.ItemsSource = dt.DefaultView;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ShowEndorsement();
        }
    }
}
