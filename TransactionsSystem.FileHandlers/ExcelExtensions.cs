using CsvHelper;
using CsvHelper.Excel;
using System.Globalization;
using TransactionsSystem.Domain.Dto;
using TransactionsSystem.Domain.Entities;

namespace TransactionsSystem.FileHandlers
{
    public class ExcelExtensions : IExcelExtensions
    {
        public List<TransactionDto> Reader(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            using var stream = new MemoryStream(bytes);
            using var parser = new ExcelParser(stream);
            using var reader = new CsvReader(parser);
            reader.Configuration.CultureInfo = CultureInfo.GetCultureInfo("en-GB");
            return reader.GetRecords<TransactionDto>().ToList();
        }
    }
}
