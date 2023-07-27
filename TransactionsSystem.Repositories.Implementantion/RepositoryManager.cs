using Microsoft.Extensions.DependencyInjection;
using TransactionsSystem.DataAccess;
using TransactionsSystem.Repositories.Contracts;
using TransactionsSystem.Repositories.Contracts.Repositories;

namespace TransactionsSystem.Repositories.Implementantion
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public RepositoryManager(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public ITransactionRepository TransactionRepository => _serviceProvider.GetRequiredService<ITransactionRepository>();

        public async Task<bool> SaveChangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token) > 0;
        }
    }
}
