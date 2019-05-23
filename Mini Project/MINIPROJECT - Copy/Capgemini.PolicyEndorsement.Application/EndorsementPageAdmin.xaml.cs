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
using Capgemini.PolicyEndorsement.BusinessLayer;

namespace Capgemini.PolicyEndorsement.Application
{
    /// <summary>
    /// Interaction logic for EndorsementPageAdmin.xaml
    /// </summary>
    public partial class EndorsementPageAdmin : Window
    {
        public EndorsementPageAdmin()
        {
            InitializeComponent();
            DataTable endorsementDetails = PolicyBL.GetAllEndorsementBL();
            DataTable dt = endorsementDetails;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show($"No Endorsement Records");
            }
            else
            {
                dgendorsement.ItemsSource = dt.DefaultView;
            }
        }

        private void Dgendorsement_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string transID = string.Empty;
            string policyID = string.Empty;
            foreach (DataGridCellInfo di in dgendorsement.SelectedCells)
            {
                DataRowView dvr = (DataRowView)di.Item;
                transID = dvr[0].ToString();
                policyID = dvr[1].ToString();

            }
            EndorsementApprovalPage obj = new EndorsementApprovalPage();
            obj.txtPolicyID.Text = policyID;
            obj.txtTransactionID.Text = transID;
            obj.Show();
        }
    }
}
