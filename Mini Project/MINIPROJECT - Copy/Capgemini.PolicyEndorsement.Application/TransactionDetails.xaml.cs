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
    /// Interaction logic for TransactionDetails.xaml
    /// </summary>
    public partial class TransactionDetails : Window
    {
        public TransactionDetails()
        {
            InitializeComponent();
            DataTable customerDetails = PolicyBL.GetAllTransactionBL();
            DataTable dt = customerDetails;
            dgTrans.ItemsSource = dt.DefaultView;
        }
    }
}
