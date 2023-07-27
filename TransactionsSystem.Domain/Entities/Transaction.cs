namespace TransactionsSystem.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string ClientName { get; set; }
        public string? Amount { get; set; }
    }
}
