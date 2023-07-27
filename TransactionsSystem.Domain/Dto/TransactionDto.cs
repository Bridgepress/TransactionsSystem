namespace TransactionsSystem.Domain.Dto
{
    public class TransactionDto
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string ClientName { get; set; }
        public string Amount { get; set; }
    }
}
