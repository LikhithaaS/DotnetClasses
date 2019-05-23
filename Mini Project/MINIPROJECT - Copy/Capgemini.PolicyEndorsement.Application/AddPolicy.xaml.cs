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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Capgemini.PolicyEndorsement.Entities;
using Capgemini.PolicyEndorsement.BusinessLayer;
using Capgemini.PolicyEndorsement.Exceptions;

namespace Capgemini.PolicyEndorsement.Application
{
    /// <summary>
    /// Interaction logic for AddPolicy.xaml
    /// </summary>
    public partial class AddPolicy : Window
    {
        string strcon = ConfigurationManager.ConnectionStrings["sqlpracticeConn"].ConnectionString;
        SqlConnection connection;
        SqlCommand cmdGetID, cmdGetPID;
        public AddPolicy()
        {
            InitializeComponent();
        }
        public void CustomerIDGen()
        {
            connection = new SqlConnection(strcon);
            connection.Open();
            cmdGetID = new SqlCommand("prcCustomerID ", connection);
            cmdGetID.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmdGetID.ExecuteReader();
            while (dr.Read())
            {
                string id = dr.GetString(0);
                cmbCustomerNumber.Items.Add(id);
            }
            connection.Close();
        }

        private void CmbCustomerNumber_DropDownClosed(object sender, EventArgs e)
        {
            connection = new SqlConnection(strcon);
            connection.Open();
            cmdGetPID = new SqlCommand("prcProductID", connection);
            cmdGetPID.CommandType = CommandType.StoredProcedure;
            cmdGetPID.Parameters.AddWithValue("@customerNumber", cmbCustomerNumber.Text);
            SqlDataReader dr = cmdGetPID.ExecuteReader();
            while (dr.Read())
            {
                string pid = dr.GetString(0);
                cmbProductID.Items.Add(pid);
            }
            connection.Close();
            int count = cmbProductID.Items.Count;

            if (count == 0)
            {
                MessageBox.Show($"No Products Exists for {cmbCustomerNumber.Text}. Please add Product to continue...!!");
                btnSubmit.IsEnabled = false;
            }

            connection = new SqlConnection(strcon);
            connection.Open();
            SqlCommand cmdGetAge = new SqlCommand("prcAgeDob ", connection);
            cmdGetAge.CommandType = CommandType.StoredProcedure;
            cmdGetAge.Parameters.AddWithValue("@customerNumber", cmbCustomerNumber.Text);
            SqlDataReader dt = cmdGetAge.ExecuteReader();
            while (dt.Read())
            {
                int age = dt.GetInt32(0);
                txtInsuredAge.Text = age.ToString();
            }
            connection.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Policy policy = new Policy();
                policy.CustomerNumber = cmbCustomerNumber.Text;
                policy.ProductID = cmbProductID.Text;
                policy.InsuredName = txtInsuredName.Text;
                policy.InsuredAge = Convert.ToInt32(txtInsuredAge.Text);
                policy.Nominee = txtNominee.Text;
                policy.Relation = txtRelation.Text;
                if (rdAnnu.IsChecked == true)
                {
                    policy.PremiumFrequency = "Annually";
                }
                else if (rdHalfY.IsChecked == true)
                {
                    policy.PremiumFrequency = "Half Yearly";
                }
                else if (rdMon.IsChecked == true)
                {
                    policy.PremiumFrequency = "Monthly";
                }
                else if (rdQuat.IsChecked == true)
                {
                    policy.PremiumFrequency = "Quaterly";
                }

                bool res = PolicyBL.AddPolicyBL(policy);
                MessageBox.Show("Policy Details have been added successfully");
                BtnClear_Click(sender, e);
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CustomerIDGen();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            btnSubmit.IsEnabled = true;
            cmbCustomerNumber.Text = "";
            cmbProductID.Items.Clear();
            txtInsuredName.Clear();
            txtInsuredAge.Clear();
            txtNominee.Clear();
            txtRelation.Clear();
            rdAnnu.IsChecked = false;
            rdHalfY.IsChecked = false;
            rdMon.IsChecked = false;
            rdQuat.IsChecked = false;
        }
    }
}
