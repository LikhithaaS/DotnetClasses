using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.PolicyEndorsement.Entities
{
    public class Product
    {
        public string ProductName { get; set; }
        public string ProductID { get; set; }
        public string ProductType { get; set; }
        public string CustomerNumber { get; set; }
        public string CreateID { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateID { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
