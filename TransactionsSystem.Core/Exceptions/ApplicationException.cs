using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsSystem.Core.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        public int StatusCode { get; }

        protected ApplicationException(int statusCode, string message, Exception? inner = null)
            : base(message, inner)
        {
            StatusCode = statusCode;
        }
    }
}
