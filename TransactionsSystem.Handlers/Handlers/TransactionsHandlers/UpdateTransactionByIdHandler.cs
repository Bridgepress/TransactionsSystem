using MediatR;
using Microsoft.EntityFrameworkCore;
using TransactionsSystem.Core.Exceptions.NotFound404;
using TransactionsSystem.Domain.Commands.Transactions;
using TransactionsSystem.Domain.Entities;
using TransactionsSystem.Repositories.Contracts;

namespace TransactionsSystem.Handlers.Handlers.TransactionsHandlers
{
    public class UpdateTransactionByIdHandler : IRequestHandler<UpdateTransactionCommand>
    {
        private readonly IRepositoryManager _repositoryManager;

        public UpdateTransactionByIdHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repositoryManager.TransactionRepository.GetAll()
                             .FirstOrDefaultAsync(x => x.Id == x.Id, cancellationToken)
                         ?? throw new NotFoundByIdException(typeof(Transaction), request.Id);
            entity.Status = request.Status;
            _repositoryManager.TransactionRepository.Update(entity);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
        }
    }
}
