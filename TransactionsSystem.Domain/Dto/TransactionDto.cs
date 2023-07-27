namespace TransactionsSystem.Domain.Dto
{
    public class TransactionDto
    {
        public string TransactionId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Buyer { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
