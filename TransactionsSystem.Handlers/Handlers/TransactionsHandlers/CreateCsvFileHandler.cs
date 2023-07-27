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
            .Search(c => c.ClientName, request.ClientName);
            if (!String.IsNullOrEmpty(request.Status))
            {
                query = query.Where(x => x.Status == request.Status);
            }
            if (!String.IsNullOrEmpty(request.Type))
            {
                query = query.Where(x => x.Type == request.Type);
            }
            var transactions = await query
                .Select(c => new Transaction
                {
                    TransactionId = c.TransactionId,
                    ClientName  = c.ClientName,
                    Type = c.Type,
                    Status = c.Status,
                    Amount = c.Amount,
                }).ToListAsync(cancellationToken);
            byte[] data =_csvExtensions.Create(transactions);

            return new GetCsvTransactionsResponse() { FileData = data };
        }
    }
}
