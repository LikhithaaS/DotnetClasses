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
    /// Interaction logic for AddProducts.xaml
    /// </summary>
    public partial class AddProducts : Window
    {
        public AddProducts()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ProductName = txtProductName.Text;
                product.ProductType = cmbProductType.Text;
                product.CustomerNumber = cmbCustomerNo.Text;
                bool res = PolicyBL.AddProductBL(product);
                MessageBox.Show("Product Details have been added successfully");
                Clear_Click(sender, e);
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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtProductName.Clear();
            cmbCustomerNo.Text = "";
            cmbProductType.Text = "";
            txtProductName.Focus();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            string strcon = ConfigurationManager.ConnectionStrings["sqlpracticeConn"].ConnectionString;
            SqlConnection connection;
            SqlCommand cmdGetID;
            connection = new SqlConnection(strcon);
            connection.Open();
            cmdGetID = new SqlCommand("prcCustomerID ", connection);
            cmdGetID.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmdGetID.ExecuteReader();
            while (dr.Read())
            {
                string id = dr.GetString(0);
                cmbCustomerNo.Items.Add(id);
            }
            connection.Close();
            cmbProductType.Items.Add("Life");
            cmbProductType.Items.Add("Non-Life");
        }
    }
}
