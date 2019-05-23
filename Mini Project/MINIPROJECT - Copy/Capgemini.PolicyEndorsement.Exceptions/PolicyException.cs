using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.PolicyEndorsement.Exceptions
{
    public class PolicyException:ApplicationException
    {
        public PolicyException() : base()
        {
        }
        public PolicyException(string message) : base(message)
        {

        }
        public PolicyException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
