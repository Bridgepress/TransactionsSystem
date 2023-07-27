using MediatR;
using TransactionsSystem.Domain.Responses.Transactions;

namespace TransactionsSystem.Domain.Requests.Transactions
{
    public class CreateCsvTransactionsParameters : IRequest<GetCsvTransactionsResponse>
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string? Buyer { get; set; }
    }
}
