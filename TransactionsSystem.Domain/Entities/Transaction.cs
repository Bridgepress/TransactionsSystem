namespace TransactionsSystem.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public string TransactionId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Buyer { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
