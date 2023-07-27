using MediatR;
using TransactionsSystem.Domain.Requests.Transactions;
using TransactionsSystem.Domain.Responses;
using TransactionsSystem.Domain.Responses.Transactions;
using TransactionsSystem.FileHandlers;
using TransactionsSystem.Repositories.Contracts;
using TransactionsSystem.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using TransactionsSystem.Domain.Entities;
using AutoMapper;

namespace TransactionsSystem.Handlers.Handlers.TransactionsHandlers
{
    public class CreateCsvFileHandler : IRequestHandler<CreateCsvTransactionsParameters, GetCsvTransactionsResponse>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICsvExtensions _csvExtensions;
        private readonly IMapper _mapper;

        public CreateCsvFileHandler(IRepositoryManager repositoryManager, ICsvExtensions csvExtensions, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _csvExtensions = csvExtensions; 
            _mapper = mapper;
        }

        public async Task<GetCsvTransactionsResponse> Handle(CreateCsvTransactionsParameters request, CancellationToken cancellationToken)
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
            var transactions = await query
                .Select(c => new Transaction
                {
                    TransactionId = c.TransactionId,
                    Buyer = c.Buyer,
                    DateUpdate = c.DateUpdate,
                    Status = c.Status,
                    Name = c.Name,
                }).ToListAsync(cancellationToken);
            byte[] data =_csvExtensions.Create(transactions);

            return new GetCsvTransactionsResponse() { FileData = data };
        }
    }
}
