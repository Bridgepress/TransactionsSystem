namespace TransactionsSystem.Core.Exceptions.NotFound404
{
    public class NotFoundByIdException : ApplicationException
    {
        public NotFoundByIdException(Type objectType, Guid id)
            : base(404, $"Object of type {objectType} with id = {id} not found")
        {
        }
    }
}
