using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Capgemini.PolicyEndorsement.Entities;
using Capgemini.PolicyEndorsement.Exceptions;
using System.Reflection;
using System.Data.Entity.Core.Objects;

namespace Capgemini.PolicyEndorsement.DataAccessLayer
{
    public class PolicyDAL
    {
        PolicyEntities entities = new PolicyEntities();
        DataTable dt;

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public DataTable GetAllEndorsementDAL()
        {
            ObjectResult<prcEndorsementDetails_Result> dr = entities.prcEndorsementDetails();
            List<prcEndorsementDetails_Result> lst = dr.ToList();
            DataTable dt = ToDataTable(lst);
            return dt;
        }      
        public string CustIDGenDAL(string policyId)
        {
            string result = entities.prccustIDGen(policyId).FirstOrDefault();
            return result;
        }

        public DataTable GetAllEndorsementCustDAL(string custnum)
        {
            ObjectResult<prcEndorsementDetailsCust_Result> dr = entities.prcEndorsementDetailsCust(custnum);
            List<prcEndorsementDetailsCust_Result> lst = dr.ToList();
            DataTable dt = ToDataTable(lst);
            return dt;
        }
        public DataTable GetAllTransactionIDDAL(string custnum)
        {
            ObjectResult<prcTransactionDetailsID_Result> dr = entities.prcTransactionDetailsID(custnum);
            List<prcTransactionDetailsID_Result> lst = dr.ToList();
            DataTable dt = ToDataTable(lst);
            return dt;
        }

        public DataTable GetAllTransactionDAL()
        {
            ObjectResult<prcTransDetails_Result> dr = entities.prcTransDetails();
            List<prcTransDetails_Result> lst = dr.ToList();
            DataTable dt = ToDataTable(lst);
            return dt;
        }


        public bool AddTransactionDAL(Transaction transaction)
        {
            bool recordAdded = false;
            try
            {                
               entities.prcTransactionDetails(transaction.TransactionID,
               transaction.PolicyID,
               transaction.InsuredName,
               transaction.InsuredAge,
               transaction.Dob,
               Convert.ToString(transaction.Gender),
               transaction.Nominee,
               transaction.Relation,
               transaction.Smoker,
               transaction.Address,
               transaction.Telephone,
               transaction.PremiumFrequency,
               transaction.StatusID,      
               transaction.CurrentStatus);
                recordAdded = true;               
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return recordAdded;
        }


        public bool UpdateCustomersDAL(Customer customer)
        {
            bool recordupdated = false;
            try
            {                
                entities.prcCustomerUpdate(
               customer.CustomerNumber,
               customer.Dob,
               customer.Gender,
               customer.Smoker,
               customer.Address,
               customer.Telephone);
                recordupdated = true;                
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return recordupdated;
        }

        public bool UpdatePolicyDAL(Policy policy)
        {
            bool recordupdated = false;
            try
            {                
                entities.prcPolicyUpdate(
                policy.PolicyID,
                policy.InsuredName,
                policy.InsuredAge,
                policy.Nominee,
                policy.Relation,
                policy.PremiumFrequency);
                recordupdated = true;                
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return recordupdated;
        }
        public bool UpdateEndorsementstatusDAL(int id, string status)
        {
            bool recordupdated = false;
            try
            {
                
                entities.prcStatusUpdateddd(id,status);
                recordupdated = true;               

            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return recordupdated;
        }
        public DataTable LoginDetailsDAL(string username, string password)
        {
            ObjectResult<prcLoginDetails_Result> dr = entities.prcLoginDetails(username, password);
            List<prcLoginDetails_Result> lst = dr.ToList();
            DataTable dt = ToDataTable(lst);
            return dt;
        }

        public bool UpdateEndorsementDAL(Endorsement endorsement)
        {
            bool recordupdated = false;
            try
            {                
                entities.prcEndorsementUpdateDetails(
                endorsement.PolicyID,
                endorsement.InsuredName,
                endorsement.InsuredAge,
                endorsement.Dob,
                endorsement.Gender,
                endorsement.Nominee,
                endorsement.Relation,
                endorsement.Smoker,
                endorsement.Address,
                endorsement.Telephone,
                endorsement.PremiumFrequency);
                recordupdated = true;               
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return recordupdated;
        }
        public DataTable GetAllEndorsementPolicyIDDAL(string policyID)
        {
            ObjectResult<prcEndorsementPolicyIDDetails_Result> dr = entities.prcEndorsementPolicyIDDetails(policyID);
            List<prcEndorsementPolicyIDDetails_Result> lst = dr.ToList();
            DataTable dt = ToDataTable(lst);
            return dt;
        }


        public bool AddEndorsementDAL(Endorsement endorsement)
        {
            bool recordAdded = false;
            try
            {               
                entities.prcEndorsementInsert( 
                endorsement.PolicyID,
                endorsement.ProductType,
                endorsement.ProductName,
                endorsement.InsuredName,
                endorsement.InsuredAge,
                endorsement.Dob,
                endorsement.Gender,
                endorsement.Nominee,
                endorsement.Relation,
                endorsement.Smoker,
                endorsement.Address,
                endorsement.Telephone,
                endorsement.PremiumFrequency);
                recordAdded = true;                
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return recordAdded;
        }

        public int LoginCheckDAL(string username, string pwd)
        {
            try
            {
                int RES = 0, result;
                ObjectParameter output = new ObjectParameter("RES", typeof(int));
                entities.prcLoginCheck(username, pwd, output);
                result = Convert.ToInt32(output.Value.ToString());
                return result;
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable SearchPolicyNameDAL(string custName, DateTime dob)
        {
            try
            {
                ObjectResult<prcPolicySearchName_Result> dr = entities.prcPolicySearchName(custName, dob);
                List<prcPolicySearchName_Result> lst = dr.ToList();
                DataTable dt = ToDataTable(lst);
                return dt;
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public DataTable SearchPolicyCustDAL(string custID)
        {
            try
            {
                ObjectResult<prcPolicySearchCust_Result> dr = entities.prcPolicySearchCust(custID);
                List<prcPolicySearchCust_Result> lst = dr.ToList();
                DataTable dt = ToDataTable(lst);
                return dt;
            }
            catch (SqlException ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
        public DataTable SearchPolicyIDDAL(string policyID)
        {
            try
            {
                ObjectResult<prcPolicySearchID_Result> dr = entities.prcPolicySearchID(policyID);
                List<prcPolicySearchID_Result> lst = dr.ToList();
                DataTable dt = ToDataTable(lst);
                return dt;
            }
            catch (PolicyException ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public bool AddPolicyDAL(Policy policy)
        {
            bool policyAdded = false;
            try
            {
                
                entities.prcPolicyInsert(policy.InsuredName, 
                policy.InsuredAge, 
                policy.Nominee, 
                policy.Relation, 
                policy.PremiumFrequency, 
                policy.CustomerNumber, 
                policy.ProductID);                
                policyAdded = true;
                
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return policyAdded;
        }
        public bool AddCustomerDAL(Customer customer)
        {
            bool customerAdded = false;
            try
            {                
                entities.prcCustomerInsert(
                 customer.CustomerName,
                 customer.Address,
               customer.Telephone,
                customer.Gender,
               customer.Dob,
                customer.Smoker,
                customer.Hobbies);
                customerAdded = true;
               
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return customerAdded;
        }
        public DataTable GetAllCustomerDAL()
        {
            ObjectResult<prcCustomerDetails_Result> dr = entities.prcCustomerDetails();
            List<prcCustomerDetails_Result> lst = dr.ToList();
            DataTable dt = ToDataTable(lst);
            return dt;
        }
        public bool AddProductDAL(Product product)
        {
            bool productAdded = false;
            try
            {
                
                entities.prcProductInsert(
                product.CustomerNumber,
                product.ProductName,
                 product.ProductType);
                productAdded = true;
                
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return productAdded;

        }
        public bool LoginGenDAL(Login login)
        {
            bool loginGenerated = false;
            try
            {
                
                entities.prcLoginGen(
                 login.CustomerNumber,
                login.LoginID,
                login.Password);
                loginGenerated = true;
                
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            return loginGenerated;

        }
    }
}
