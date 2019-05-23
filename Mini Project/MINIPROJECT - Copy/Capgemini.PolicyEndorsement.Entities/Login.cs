using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.PolicyEndorsement.Entities
{
    public class Login
    {
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string CustomerNumber { get; set; }
        public string CreateID { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateID { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
