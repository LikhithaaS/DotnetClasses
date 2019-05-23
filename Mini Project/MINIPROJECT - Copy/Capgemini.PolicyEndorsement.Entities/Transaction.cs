using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.PolicyEndorsement.Entities
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public string PolicyID { get; set; }
        public string InsuredName { get; set; }
        public int InsuredAge { get; set; }
        public DateTime Dob { get; set; }
        public Char Gender { get; set; }
        public string Nominee { get; set; }
        public string Relation { get; set; }
        public string Smoker { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string PremiumFrequency { get; set; }
        public int StatusID { get; set; }
        public string CurrentStatus { get; set; }
    }
}
