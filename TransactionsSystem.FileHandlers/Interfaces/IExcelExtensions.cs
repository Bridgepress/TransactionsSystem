using TransactionsSystem.Domain.Dto;

namespace TransactionsSystem.FileHandlers.Interfaces
{
    public interface IExcelExtensions
    {
        List<TransactionDto> Reader(string base64);
    }
}
