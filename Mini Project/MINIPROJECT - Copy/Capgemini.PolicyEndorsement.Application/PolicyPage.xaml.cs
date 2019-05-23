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
    /// Interaction logic for PolicyPage.xaml
    /// </summary>
    public partial class PolicyPage : Window
    {
        public PolicyPage()
        {
            InitializeComponent();
        }
        public void SearchPolicy(string policyID)
        {
            DataTable dt = PolicyBL.SearchPolicyIDBL(policyID);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtproductType.Text = row["ProductType"].ToString();
                    txtproductName.Text = row["ProductName"].ToString();
                    txtinsuredName.Text = row["InsuredName"].ToString();
                    txtinsuredAge.Text = row["InsuredAge"].ToString();
                    txtdob.Text = row["Dob"].ToString();
                    txtnominee.Text = row["Nominee"].ToString();
                    txtRelation.Text = row["Relation"].ToString();
                    txtAddress.Text = row["Address"].ToString();
                    txtTelephone.Text = row["Telephone"].ToString();
                    string gender = row["Gender"].ToString();
                    if (gender != string.Empty)
                    {
                        if (gender.Equals("M"))
                        {
                            rdMale.IsChecked = true;
                        }
                        else if (gender.Equals("F"))
                        {
                            rdFemale.IsChecked = true;
                        }
                        else if (gender.Equals("O"))
                        {
                            rdOthers.IsChecked = true;
                        }
                    }
                    string premium = row["PremiumFrequency"].ToString();
                    if (premium != string.Empty)
                    {
                        if (premium.Equals("Monthly"))
                        {
                            rdMonth.IsChecked = true;
                        }
                        else if (premium.Equals("Half Yearly"))
                        {
                            rdHalf.IsChecked = true;
                        }
                        else if (premium.Equals("Quaterly"))
                        {
                            rdQuater.IsChecked = true;
                        }
                        else if (premium.Equals("Annually"))
                        {
                            rdAnnual.IsChecked = true;
                        }
                    }
                    string smoker = row["Smoker"].ToString();
                    if (smoker != string.Empty)
                    {
                        if (smoker.Equals("Y"))
                        {
                            rdYes.IsChecked = true;
                        }
                        else if (smoker.Equals("N"))
                        {
                            rdNo.IsChecked = true;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Records to display");
            }

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SearchPolicy(txtPolicyID.Text);
        }

        private void BtnUpdatepolicy_Click(object sender, RoutedEventArgs e)
        {
            UpdatePolicyPage obj = new UpdatePolicyPage();
            obj.txtPolicyID.Text = txtPolicyID.Text;
            obj.Show();
        }
    }
}
