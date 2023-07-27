using MediatR;
using TransactionsSystem.Domain.Requests.Transactions;
using TransactionsSystem.Domain.Responses;
using TransactionsSystem.Domain.Responses.Transactions;
using TransactionsSystem.Repositories.Contracts;
using TransactionsSystem.Core.Extensions;
using Microsoft.EntityFrameworkCore;

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
             .Search(c => c.Name, request.Name);
            if (!String.IsNullOrEmpty(request.Buyer))
            {
                query = query.Where(x => x.Buyer == request.Buyer);
            }
            if (request.DateUpdate != null)
            {
                query = query.Where(x => x.DateUpdate == request.DateUpdate);
            }
            if (!String.IsNullOrEmpty(request.Status))
            {
                query = query.Where(x => x.Status == request.Status);
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
                   Buyer = c.Buyer,
                   DateUpdate = c.DateUpdate,
                   Status = c.Status,
                   Name = c.Name,
                })
                .ToListAsync(cancellationToken);

            return new PaginationResponse<TransactionResponse>(count, pagedCourses);
        }
    }
}
