using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.PolicyEndorsement.Entities
{
    public class Customer
    {
        public string CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Smoker { get; set; }
        public string Hobbies { get; set; }
        public string CreateID { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateID { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
