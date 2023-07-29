using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using TransactionsSystem.Domain.Entities;
using TransactionsSystem.FileHandlers.Interfaces;

namespace TransactionsSystem.FileHandlers
{
    public class CsvExtensions : ICsvExtensions
    {
        public byte[] Create(List<Transaction> transactions)
        {
            var configPersons = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };
            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, configPersons))
            {
                csv.WriteRecords(transactions);
                writer.Flush(); 
                return memoryStream.ToArray(); 
            }
        }
    }
}
