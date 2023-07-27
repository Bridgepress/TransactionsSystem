namespace TransactionsSystem.Domain.Responses.Transactions
{
    public class GetCsvTransactionsResponse
    {
        public byte[] FileData { get; set; } = default!;
    }
}
