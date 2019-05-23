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
using Capgemini.PolicyEndorsement.BusinessLayer;

namespace Capgemini.PolicyEndorsement.Application
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            PolicyBL policyBL = new PolicyBL();
            int res = policyBL.LoginCheckBL(username, password);
            if (res == 1)
            {
                string id = string.Empty, custnum = string.Empty;
                DataTable dt = new DataTable();
                dt = PolicyBL.LoginDeatilsBL(txtUsername.Text, txtPassword.Password);
                foreach (DataRow row in dt.Rows)
                {
                    id = row["LoginID"].ToString();
                }
                if (id.Equals("Admin"))
                {
                    AdminHomepage obj = new AdminHomepage();
                    obj.Show();
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        custnum = row["CustomerNumber"].ToString();
                    }
                    txtCustomerNumber.Text = custnum;
                    CustomerHomepage obj1 = new CustomerHomepage();
                    obj1.txtCustNum.Text = custnum;
                    obj1.Show();
                }

            }
            else
            {
                MessageBox.Show("Login Failed. Please Check LoginID and Password");
            }

        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            txtPassword.Clear();
            txtUsername.Clear();
        }
    }
}
