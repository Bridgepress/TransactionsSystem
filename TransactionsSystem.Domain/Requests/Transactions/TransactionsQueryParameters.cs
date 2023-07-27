using MediatR;
using TransactionsSystem.Domain.Responses;
using TransactionsSystem.Domain.Responses.Transactions;

namespace TransactionsSystem.Domain.Requests.Transactions
{
    public class TransactionsQueryParameters : QueryParameters, IRequest<PaginationResponse<TransactionResponse>>
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string? Buyer { get; set; }
    }
}
