using TransactionsSystem.Domain.Dto;

namespace TransactionsSystem.FileHandlers
{
    public interface IExcelExtensions
    {
        List<TransactionDto> Reader(string base64);
    }
}
