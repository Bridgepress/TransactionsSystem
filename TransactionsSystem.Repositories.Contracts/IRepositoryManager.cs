using TransactionsSystem.Repositories.Contracts.Repositories;

namespace TransactionsSystem.Repositories.Contracts
{
    public interface IRepositoryManager
    {
        ITransactionRepository TransactionRepository { get; }

        Task<bool> SaveChangesAsync(CancellationToken token = default);
    }
}
