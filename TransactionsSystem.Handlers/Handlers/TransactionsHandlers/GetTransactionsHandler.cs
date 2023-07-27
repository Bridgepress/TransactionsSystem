using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionsSystem.Core.Extensions;
using TransactionsSystem.Domain.Requests.Transactions;
using TransactionsSystem.Domain.Responses;
using TransactionsSystem.Domain.Responses.Transactions;
using TransactionsSystem.Repositories.Contracts;

namespace TransactionsSystem.Handlers.Handlers.TransactionsHandlers
{
    public class GetTransactionsHandler :
        IRequestHandler<TransactionsQueryParameters, PaginationResponse<TransactionResponse>>
    {
        private readonly IRepositoryManager _repositoryManager;

        public GetTransactionsHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<PaginationResponse<TransactionResponse>> Handle(TransactionsQueryParameters request,
            CancellationToken cancellationToken)
        {
            var query = _repositoryManager.TransactionRepository.GetAll()
                .Search(c => c.ClientName, request.ClientName);
            if (!String.IsNullOrEmpty(request.Status))
            {
                query = query.Where(x => x.Status == request.Status);
            }
            if (!String.IsNullOrEmpty(request.Type))
            {
                query = query.Where(x => x.Type == request.Type);
            }
            var count = await query.CountAsync(cancellationToken);
            if (count == 0 || count < request.PageNumber * request.PageSize)
            {
                return new PaginationResponse<TransactionResponse>(0, Array.Empty<TransactionResponse>());
            }
            var pagedCourses = await query
                .Sort(request.OrderBy, request.SortOrder)
                .Skip(request.PageSize * request.PageNumber)
                .Take(request.PageSize)
                .Select(c => new TransactionResponse
                {
                    TransactionId = c.TransactionId,
                    ClientName = c.ClientName,
                    Amount = c.Amount,
                    Type = c.Type,
                    Status = c.Status,
                })
                .ToListAsync(cancellationToken);

            return new PaginationResponse<TransactionResponse>(count, pagedCourses);
        }
    }
}
