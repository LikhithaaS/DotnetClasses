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
using Capgemini.PolicyEndorsement.BusinessLayer;
using Capgemini.PolicyEndorsement.Entities;
using Capgemini.PolicyEndorsement.Exceptions;
using System.Data;
using System.Data.SqlClient;

namespace Capgemini.PolicyEndorsement.Application
{
    /// <summary>
    /// Interaction logic for EndorsementApprovalPage.xaml
    /// </summary>
    public partial class EndorsementApprovalPage : Window
    {
        public EndorsementApprovalPage()
        {
            InitializeComponent();

        }
        public void CustIDGen()
        {
            string result = PolicyBL.CustIDGenBL(txtPolicyID.Text);
            txtCustnum.Text = result;
        }
        public void ShowOrigData()
        {
            DataTable dt = PolicyBL.SearchPolicyIDBL(txtPolicyID.Text);
            foreach (DataRow row in dt.Rows)
            {
                txtInsuredNameOrig.Text = row["InsuredName"].ToString();
                txtInsuredAgeOrig.Text = row["InsuredAge"].ToString();
                txtDobOrig.Text = row["Dob"].ToString();
                txtGenderOrig.Text = row["Gender"].ToString();
                txtNomineeOrig.Text = row["Nominee"].ToString();
                txtRelationOrig.Text = row["Relation"].ToString();
                txtSmokerOrig.Text = row["Smoker"].ToString();
                txtAddresOrig.Text = row["Address"].ToString();
                txtTeleOrig.Text = row["Telephone"].ToString();
                txtPrFreqOrig.Text = row["PremiumFrequency"].ToString();
            }
        }
        public void DeleteEndorsement()
        {
            Transaction transaction = new Transaction();
            transaction.PolicyID = txtPolicyID.Text;
            transaction.TransactionID = Convert.ToInt32(txtTransactionID.Text);
            transaction.InsuredName = txtInsuredName.Text;
            transaction.InsuredAge = Convert.ToInt32(txtInsuredAge.Text);
            transaction.Dob = Convert.ToDateTime(txtDob.Text);
            transaction.Gender = Convert.ToChar(txtGender.Text);
            transaction.Nominee = txtNominee.Text;
            transaction.Relation = txtRelation.Text;
            transaction.Smoker = txtSmoker.Text;
            transaction.Address = txtAddress.Text;
            transaction.Telephone = txtTelephone.Text;
            transaction.PremiumFrequency = txtPreFreq.Text;
            transaction.CurrentStatus = txtStatus.Text;
            PolicyBL policyBL = new PolicyBL();
            bool res = policyBL.AddTransactionBL(transaction);
        }

        public void ShowEndorsementData()
        {
            PolicyBL searchpolicy = new PolicyBL();
            DataTable dt = searchpolicy.GetAllEndorsementPolicyIDBL(txtPolicyID.Text);
            foreach (DataRow row in dt.Rows)
            {
                txtInsuredName.Text = row["InsuredName"].ToString();
                txtInsuredAge.Text = row["InsuredAge"].ToString();
                txtDob.Text = row["Dob"].ToString();
                txtGender.Text = row["Gender"].ToString();
                txtNominee.Text = row["Nominee"].ToString();
                txtRelation.Text = row["Relation"].ToString();
                txtSmoker.Text = row["Smoker"].ToString();
                txtAddress.Text = row["Address"].ToString();
                txtTelephone.Text = row["Telephone"].ToString();
                txtPreFreq.Text = row["PremiumFrequency"].ToString();
                txtStatus.Text = row["CurrentStatus"].ToString();
            }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CustIDGen();
        }

        private void TxtPolicyID_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowOrigData();
            ShowEndorsementData();
            if (txtStatus.Text.Equals("Pending"))
            {
                txtStatus.Background = new SolidColorBrush(Colors.Yellow);
            }
            else if (txtStatus.Text.Equals("Accepted"))
            {
                txtStatus.Background = new SolidColorBrush(Colors.Green);
            }
            else if (txtStatus.Text.Equals("Rejected"))
            {
                txtStatus.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {

            txtStatus.Background = new SolidColorBrush(Colors.Green);
            txtStatus.Text = "Accepted";
            try
            {
                Endorsement endorsement = new Endorsement();
                endorsement.PolicyID = txtPolicyID.Text;
                endorsement.InsuredName = txtInsuredName.Text;
                endorsement.InsuredAge = Convert.ToInt32(txtInsuredAge.Text);
                endorsement.Dob = Convert.ToDateTime(txtDob.Text);
                endorsement.Gender = txtGender.Text;
                endorsement.Nominee = txtNominee.Text;
                endorsement.Relation = txtRelation.Text;
                endorsement.Smoker = txtSmoker.Text;
                endorsement.Address = txtAddress.Text;
                endorsement.Telephone = txtTelephone.Text;
                endorsement.PremiumFrequency = txtPreFreq.Text;
                PolicyBL policyBL = new PolicyBL();
                bool res = policyBL.UpdateEndorsementBL(endorsement);
                int id = Convert.ToInt32(txtTransactionID.Text);
                bool result = policyBL.UpdateEndorsementStatusBL(id, txtStatus.Text);

                if (res != false && result != false)
                {
                    Policy policy = new Policy();
                    policy.PolicyID = txtPolicyID.Text;
                    policy.InsuredName = txtInsuredName.Text;
                    policy.InsuredAge = Convert.ToInt32(txtInsuredAge.Text);
                    policy.Nominee = txtNominee.Text;
                    policy.Relation = txtRelation.Text;
                    policy.PremiumFrequency = txtPreFreq.Text;
                    PolicyBL policyup = new PolicyBL();
                    bool resut = policyup.UpdatePolicyBL(policy);

                    Customer customer = new Customer();
                    customer.CustomerNumber = txtCustnum.Text;
                    customer.Dob = Convert.ToDateTime(txtDob.Text);
                    customer.Gender = txtGender.Text;
                    customer.Smoker = txtSmoker.Text;
                    customer.Address = txtAddress.Text;
                    customer.Telephone = txtTelephone.Text;
                    PolicyBL policydate = new PolicyBL();
                    bool resul = policydate.UpdateCustomersBL(customer);

                    if (resut != false && resul != false)
                    {
                        DeleteEndorsement();
                    }

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

        private void BtnReject_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Background = new SolidColorBrush(Colors.Red);
            txtStatus.Text = "Rejected";
            EndorsementStatus endorsementstatus = new EndorsementStatus();
            int id = Convert.ToInt32(txtTransactionID.Text);
            endorsementstatus.CurrentStatus = txtStatus.Text;
            PolicyBL policyBL = new PolicyBL();
            bool result = policyBL.UpdateEndorsementStatusBL(id, txtStatus.Text);
            if (result != false)
            {
                DeleteEndorsement();
            }
        }
    }
}
