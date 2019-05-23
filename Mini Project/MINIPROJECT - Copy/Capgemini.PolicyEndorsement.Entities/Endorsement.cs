using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.PolicyEndorsement.Entities
{
    public class Endorsement
    {
        public int TransactionID { get; set; }
        public string PolicyID { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string InsuredName { get; set; }
        public int InsuredAge { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Nominee { get; set; }
        public string Relation { get; set; }
        public string Smoker { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string PremiumFrequency { get; set; }
        public string CreateID { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateID { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
