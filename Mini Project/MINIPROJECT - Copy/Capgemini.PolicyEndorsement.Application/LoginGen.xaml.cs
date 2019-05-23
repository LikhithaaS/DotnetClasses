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
    /// Interaction logic for LoginGen.xaml
    /// </summary>
    public partial class LoginGen : Window
    {
        public LoginGen()
        {
            InitializeComponent();
        }
        string strcon = ConfigurationManager.ConnectionStrings["sqlpracticeConn"].ConnectionString;
        SqlConnection connection;
        SqlCommand cmdGetID;
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CustomerIDGen();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Login login = new Login();
                login.CustomerNumber = cmbCustomerNumber.Text;
                login.LoginID = txtLogin.Text;
                login.Password = txtPassword.Text;
                bool res = PolicyBL.LoginGenBL(login);
                MessageBox.Show("Login Credentials Generated");
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

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtLogin.Clear();
            txtPassword.Clear();
            cmbCustomerNumber.Text = "";
        }
    }
}
