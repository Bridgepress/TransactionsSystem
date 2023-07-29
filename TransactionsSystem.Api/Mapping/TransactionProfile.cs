using AutoMapper;
using TransactionsSystem.Domain.Entities;
using TransactionsSystem.Domain.Responses.Transactions;

namespace TransactionsSystem.Api.Mapping
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionResponse>();
        }
    }
}
