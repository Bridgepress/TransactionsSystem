using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
