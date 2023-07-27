namespace TransactionsSystem.Core.Exceptions.BadRequest400
{
    public class FileEmptyException : ApplicationException
    {
        public FileEmptyException()
            : base(400, $"File empty")
        {

        }
    }
}
