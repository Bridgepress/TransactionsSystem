using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionsSystem.Core.Exceptions.BadRequest400;
using TransactionsSystem.Domain.Commands.Transactions;
using TransactionsSystem.Domain.Dto;
using TransactionsSystem.Domain.Entities;
using TransactionsSystem.FileHandlers;
using TransactionsSystem.Repositories.Contracts;

namespace TransactionsSystem.Handlers.Handlers.TransactionsHandlers
{
    public class UpdateTransactionsHandler : IRequestHandler<UpdateTransactionsCommand>
    {
        private readonly IRepositoryManager _repositoryManager;
        private IExcelExtensions _excelExtensions;

        public UpdateTransactionsHandler(IExcelExtensions reader, IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _excelExtensions = reader;
        }

        public async Task Handle(UpdateTransactionsCommand request, CancellationToken cancellationToken)
        {
            List<TransactionDto> getTransactions = _excelExtensions.Reader(request.FileData);
            if (getTransactions == null || getTransactions.Count == 0)
            {
                throw new FileEmptyException();
            }
            List<Transaction> newTransactions = new List<Transaction>();

            foreach (var item in getTransactions)
            {
                var existingTransaction = await _repositoryManager.TransactionRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.TransactionId == item.TransactionId);

                if (existingTransaction == null)
                {
                    newTransactions.Add(new Transaction()
                    {
                        Id = Guid.NewGuid(),
                        ClientName = item.ClientName,
                        Status = item.Status,
                        Type = item.Type,
                        Amount = item.Amount,
                        TransactionId = item.TransactionId,
                    });
                }
                else
                {
                    existingTransaction.ClientName = item.ClientName;
                    existingTransaction.Status = item.Status;
                    existingTransaction.Type = item.Type;
                    existingTransaction.Amount = item.Amount;
                }
            }

            _repositoryManager.TransactionRepository.AddRange(newTransactions);
            await _repositoryManager.SaveChangesAsync();
        }
    }
}