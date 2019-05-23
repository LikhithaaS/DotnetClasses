using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.PolicyEndorsement.Entities
{
    public class Policy
    {
        public string PolicyID { get; set; }
        public string InsuredName { get; set; }
        public int InsuredAge { get; set; }
        public string Nominee { get; set; }
        public string Relation { get; set; }
        public string PremiumFrequency { get; set; }
        public string CustomerNumber { get; set; }
        public string ProductID { get; set; }
        public string CreateID { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateID { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
