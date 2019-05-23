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
using System.Configuration;
using System.Data.SqlClient;
using Capgemini.PolicyEndorsement.BusinessLayer;
using Capgemini.PolicyEndorsement.Entities;
using Capgemini.PolicyEndorsement.Exceptions;

namespace Capgemini.PolicyEndorsement.Application
{
    /// <summary>
    /// Interaction logic for UpdatePolicyPage.xaml
    /// </summary>
    public partial class UpdatePolicyPage : Window
    {
        public UpdatePolicyPage()
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
                    dpDob.Text = row["Dob"].ToString();
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

        private void BtnUpdat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Endorsement endorsement = new Endorsement();
                endorsement.PolicyID = txtPolicyID.Text;
                endorsement.ProductType = txtproductType.Text;
                endorsement.ProductName = txtproductName.Text;
                endorsement.InsuredName = txtinsuredName.Text;
                endorsement.InsuredAge = Convert.ToInt32(txtinsuredAge.Text);
                endorsement.Dob = Convert.ToDateTime(dpDob.Text);
                if (rdMale.IsChecked == true)
                {
                    endorsement.Gender = "M";
                }
                else if (rdFemale.IsChecked == true)
                {
                    endorsement.Gender = "F";
                }
                else
                {
                    endorsement.Gender = "O";
                }
                endorsement.Nominee = txtnominee.Text;
                endorsement.Relation = txtRelation.Text;
                endorsement.Smoker = rdYes.IsChecked == true ? "Y" : "N";
                endorsement.Address = txtAddress.Text;
                endorsement.Telephone = txtTelephone.Text;
                if (rdAnnual.IsChecked == true)
                {
                    endorsement.PremiumFrequency = "Annually";
                }
                else if (rdHalf.IsChecked == true)
                {
                    endorsement.PremiumFrequency = "Half Yearly";
                }
                else if (rdMonth.IsChecked == true)
                {
                    endorsement.PremiumFrequency = "Monthly";
                }
                else if (rdQuater.IsChecked == true)
                {
                    endorsement.PremiumFrequency = "Quaterly";
                }
                PolicyBL policyBL = new PolicyBL();
                bool res = policyBL.AddEndorsementBL(endorsement);
                if (res != false)
                {
                    int tid = 0;
                    string strcon = ConfigurationManager.ConnectionStrings["sqlpracticeConn"].ConnectionString;
                    SqlConnection connection;
                    SqlCommand cmdGetID, cmdInsert;
                    connection = new SqlConnection(strcon);
                    connection.Open();
                    cmdGetID = new SqlCommand("prctransactionID", connection);
                    cmdGetID.CommandType = CommandType.StoredProcedure;
                    cmdGetID.Parameters.AddWithValue("@policyID", txtPolicyID.Text);
                    SqlDataReader dr = cmdGetID.ExecuteReader();
                    while (dr.Read())
                    {
                        tid = dr.GetInt32(0);
                        MessageBox.Show("TransactionID: " + tid + " Generated for Update request. \n Administrator will approve or reject the request");
                    }
                    connection.Close();
                    connection = new SqlConnection(strcon);
                    connection.Open();
                    cmdInsert = new SqlCommand("prcStatusupdate", connection);
                    cmdInsert.CommandType = CommandType.StoredProcedure;
                    cmdInsert.Parameters.AddWithValue("@transactionID", tid);
                    cmdInsert.ExecuteNonQuery();
                }

            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (PolicyException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
