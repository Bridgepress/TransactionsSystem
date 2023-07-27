using System.Net;

namespace TransactionsSystem.Core.Exceptions.NotFound404
{
    public class NotFoundUserByUserNameException : ApplicationException
    {
        public NotFoundUserByUserNameException(string email)
            : base((int)HttpStatusCode.NotFound, $"User with UserName {email} not found")
        {
        }
    }
}
