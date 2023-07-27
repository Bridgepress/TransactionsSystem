using TransactionsSystem.DataAccess;
using TransactionsSystem.Domain.Entities;
using TransactionsSystem.Repositories.Contracts.Repositories;

namespace TransactionsSystem.Repositories.Implementantion.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
