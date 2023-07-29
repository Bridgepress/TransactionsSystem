using TransactionsSystem.Domain.Entities;

namespace TransactionsSystem.FileHandlers.Interfaces
{
    public interface ICsvExtensions
    {
        byte[] Create(List<Transaction> transactions);
    }
}
