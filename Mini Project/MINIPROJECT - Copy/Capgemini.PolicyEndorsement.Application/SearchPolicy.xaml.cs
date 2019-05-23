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
using Capgemini.PolicyEndorsement.Entities;
using Capgemini.PolicyEndorsement.Exceptions;

namespace Capgemini.PolicyEndorsement.Application
{
    /// <summary>
    /// Interaction logic for SearchPolicy.xaml
    /// </summary>
    public partial class SearchPolicy : Window
    {
        public SearchPolicy()
        {
            InitializeComponent();
        }

        private void DgPolicy_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string policyID = string.Empty;
            foreach (DataGridCellInfo di in dgPolicy.SelectedCells)
            {
                DataRowView dvr = (DataRowView)di.Item;
                policyID = dvr[0].ToString();

            }
            PolicyPage obj = new PolicyPage();

            obj.txtPolicyID.Text = policyID;
            obj.Show();
        }

        private void DgPolicyCNum_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string policyID = string.Empty;
            foreach (DataGridCellInfo di in dgPolicyCNum.SelectedCells)
            {
                DataRowView dvr = (DataRowView)di.Item;
                policyID = dvr[0].ToString();

            }
            PolicyPage obj1 = new PolicyPage();

            obj1.txtPolicyID.Text = policyID;
            obj1.Show();
        }

        private void DgPolicyName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string policyID = string.Empty;
            foreach (DataGridCellInfo di in dgPolicyName.SelectedCells)
            {
                DataRowView dvr = (DataRowView)di.Item;
                policyID = dvr[0].ToString();

            }
            PolicyPage obj2 = new PolicyPage();

            obj2.txtPolicyID.Text = policyID;
            obj2.Show();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataTable search = PolicyBL.SearchPolicyIDBL(txtpolicyID.Text);
                DataTable dt = search;
                if (dt == null)
                {
                    MessageBox.Show($"No Records found for {txtpolicyID.Text}");
                }
                else if (dt.Rows.Count == 0)
                {
                    MessageBox.Show($"No Records found for {txtpolicyID.Text}");
                }
                else
                {
                    dgPolicy.ItemsSource = dt.DefaultView;
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

        private void BtnSearch1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = PolicyBL.SearchPolicyCustBL(txtCustNum.Text);
                if (dt == null)
                {
                    MessageBox.Show($"No Records found for {txtCustNum.Text}");
                }
                else if (dt.Rows.Count == 0)
                {
                    MessageBox.Show($"No Records found for {txtCustNum.Text}");
                }
                else
                {
                    dgPolicyCNum.ItemsSource = dt.DefaultView;
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

        private void BtnSearch2_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == string.Empty || txtDob.Text == string.Empty)
            {
                MessageBox.Show("Please enter both the fields");
            }
            else
            {
                DateTime dofb = Convert.ToDateTime(txtDob.Text);
                try
                {
                    DataTable dt = PolicyBL.SearchPolicyNameBL(txtName.Text, dofb);
                    if (dt == null)
                    {
                        MessageBox.Show($"No Records found for {txtName.Text}");
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show($"No Records found for {txtName.Text}");
                    }
                    else
                    {
                        dgPolicyName.ItemsSource = dt.DefaultView;
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
}
