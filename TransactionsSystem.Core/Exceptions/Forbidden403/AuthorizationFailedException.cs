using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsSystem.Core.Exceptions.Forbidden403
{
    public class AuthorizationFailedException : ApplicationException
    {
        public AuthorizationFailedException()
            : base((int)HttpStatusCode.Forbidden, "UserName or Password is incorrect")
        {
        }
    }
}
