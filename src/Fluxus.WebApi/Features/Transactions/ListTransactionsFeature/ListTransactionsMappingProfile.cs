using AutoMapper;
using Fluxus.Application.Features.Transactions.ListTransactions;

namespace Fluxus.WebApi.Features.Transactions.ListTransactionsFeature
{
    public class ListTransactionsMappingProfile : Profile
    {
        public ListTransactionsMappingProfile()
        {
            CreateMap<ListTransactionsRequest, ListTransactionsQuery>();
        }
    }
}
