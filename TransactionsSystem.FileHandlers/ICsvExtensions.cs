using TransactionsSystem.Domain.Entities;

namespace TransactionsSystem.FileHandlers
{
    public interface ICsvExtensions
    {
        byte[] Create(List<Transaction> transactions);
    }
}
