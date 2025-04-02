using AutoMapper;
using Fluxus.Domain.Entities;

namespace Fluxus.Application.Features.Transactions.ListTransactions
{
    public class ListTransactionsProfile : Profile
    {
        public ListTransactionsProfile()
        {
            CreateMap<Transaction, ListTransactionsResult>();
        }
    }
}
