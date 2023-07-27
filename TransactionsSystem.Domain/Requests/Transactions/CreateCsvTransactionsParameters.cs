using MediatR;
using TransactionsSystem.Domain.Responses.Transactions;

namespace TransactionsSystem.Domain.Requests.Transactions
{
    public class CreateCsvTransactionsParameters : IRequest<GetCsvTransactionsResponse>
    {
        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? ClientName { get; set; }
        public decimal? Amount { get; set; }
    }
}
