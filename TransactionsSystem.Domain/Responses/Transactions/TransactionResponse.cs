namespace TransactionsSystem.Domain.Responses.Transactions
{
    public class TransactionResponse
    {
        public string TransactionId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Buyer { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
