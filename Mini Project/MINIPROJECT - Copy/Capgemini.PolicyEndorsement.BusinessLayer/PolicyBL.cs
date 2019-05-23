using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Capgemini.PolicyEndorsement.Entities;
using Capgemini.PolicyEndorsement.Exceptions;
using Capgemini.PolicyEndorsement.DataAccessLayer;

namespace Capgemini.PolicyEndorsement.BusinessLayer
{
    public class PolicyBL
    {
        public static string CustIDGenBL(string policyId)
        {
            PolicyDAL customernumber = new PolicyDAL();
            string result = customernumber.CustIDGenDAL(policyId);
            return result;
        }
        public static DataTable LoginDeatilsBL(string username, string password)
        {
            DataTable dataTable;
            PolicyDAL endorsementDAL = new PolicyDAL();
            dataTable = endorsementDAL.LoginDetailsDAL(username, password);
            return dataTable;
        }
        public static DataTable GetAllEndorsementCustBL(string custnum)
        {
            DataTable dataTable;
            PolicyDAL endorsement = new PolicyDAL();
            dataTable = endorsement.GetAllEndorsementCustDAL(custnum);
            return dataTable;
        }

        public static DataTable GetAllTransactionIDBL(string custnum)
        {
            DataTable dataTable;
            PolicyDAL endorsementDAL = new PolicyDAL();
            dataTable = endorsementDAL.GetAllTransactionIDDAL(custnum);
            return dataTable;
        }
        public static DataTable GetAllTransactionBL()
        {
            DataTable dataTable;
            PolicyDAL endorsementDAL = new PolicyDAL();
            dataTable = endorsementDAL.GetAllTransactionDAL();
            return dataTable;
        }

        public bool AddTransactionBL(Transaction transaction)
        {
            bool recordAdded = false;
            try
            {
                PolicyDAL customerDAL = new PolicyDAL();
                recordAdded = customerDAL.AddTransactionDAL(transaction);

            }
            catch (PolicyException)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recordAdded;
        }
        public bool UpdateCustomersBL(Customer customer)
        {
            bool recordupdated = false;
            try
            {
                PolicyDAL customerDAL = new PolicyDAL();
                recordupdated = customerDAL.UpdateCustomersDAL(customer);

            }
            catch (PolicyException)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recordupdated;
        }

        public bool UpdatePolicyBL(Policy policy)
        {
            bool recordupdated = false;
            try
            {
                PolicyDAL customerDAL = new PolicyDAL();
                recordupdated = customerDAL.UpdatePolicyDAL(policy);

            }
            catch (PolicyException)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recordupdated;
        }
        public bool UpdateEndorsementBL(Endorsement endorsement)
        {
            bool recordupdated = false;
            try
            {
                PolicyDAL customerDAL = new PolicyDAL();
                recordupdated = customerDAL.UpdateEndorsementDAL(endorsement);

            }
            catch (PolicyException)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recordupdated;
        }
        public bool UpdateEndorsementStatusBL(int id, string status)
        {
            bool recordupdated = false;
            try
            {
                PolicyDAL customerDAL = new PolicyDAL();
                recordupdated = customerDAL.UpdateEndorsementstatusDAL(id, status);

            }
            catch (PolicyException)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recordupdated;
        }
        public DataTable GetAllEndorsementPolicyIDBL(string policyID)
        {
            DataTable dataTable;
            PolicyDAL endorsementDAL = new PolicyDAL();
            dataTable = endorsementDAL.GetAllEndorsementPolicyIDDAL(policyID);
            return dataTable;
        }
        public static DataTable GetAllEndorsementBL()
        {
            DataTable dataTable;
            PolicyDAL endorsementDAL = new PolicyDAL();
            dataTable = endorsementDAL.GetAllEndorsementDAL();
            return dataTable;
        }
        public bool AddEndorsementBL(Endorsement endorsement)
        {
            bool recordAdded = false;
            try
            {
                PolicyDAL customerDAL = new PolicyDAL();
                recordAdded = customerDAL.AddEndorsementDAL(endorsement);

            }
            catch (PolicyException)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return recordAdded;
        }
        public int LoginCheckBL(string username, string password)
        {
            PolicyDAL logincheck = new PolicyDAL();
            int res = logincheck.LoginCheckDAL(username, password);
            return res;
        }
        public static DataTable SearchPolicyNameBL(string custName, DateTime dob)
        {
            PolicyDAL searchpolicy = new PolicyDAL();
            DataTable dt = new DataTable();
            dt = searchpolicy.SearchPolicyNameDAL(custName, dob);
            return dt;
        }
        public static DataTable SearchPolicyCustBL(string custID)
        {
            PolicyDAL searchpolicy = new PolicyDAL();
            DataTable dt = new DataTable();
            dt = searchpolicy.SearchPolicyCustDAL(custID);
            return dt;
        }
        public static DataTable SearchPolicyIDBL(string policyID)
        {
            PolicyDAL searchpolicy = new PolicyDAL();
            DataTable policy = searchpolicy.SearchPolicyIDDAL(policyID);

            return policy;
        }
        public static bool LoginGenBL(Login login)
        {
            bool loginGenerated = false;
            try
            {
                PolicyDAL loginDAL = new PolicyDAL();
                loginGenerated = loginDAL.LoginGenDAL(login);

            }
            catch (PolicyException)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return loginGenerated;
        }
        public static bool AddPolicyBL(Policy policy)
        {
            bool policyAdded = false;
            try
            {
                PolicyDAL policyDAL = new PolicyDAL();
                policyAdded = policyDAL.AddPolicyDAL(policy);

            }
            catch (PolicyException)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return policyAdded;
        }
        public static bool AddCustomerBL(Customer customer)
        {
            bool customerAdded = false;
            try
            {
                PolicyDAL customerDAL = new PolicyDAL();
                customerAdded = customerDAL.AddCustomerDAL(customer);

            }
            catch (PolicyException)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerAdded;
        }
        public static DataTable GetAllCustomerBL()
        {
            DataTable dataTable;
            PolicyDAL customerDAL = new PolicyDAL();
            dataTable = customerDAL.GetAllCustomerDAL();
            return dataTable;
        }
        public static bool AddProductBL(Product product)
        {
            bool productAdded = false;
            try
            {
                PolicyDAL productDAL = new PolicyDAL();
                productAdded = productDAL.AddProductDAL(product);

            }
            catch (PolicyException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productAdded;
        }
    }
}
