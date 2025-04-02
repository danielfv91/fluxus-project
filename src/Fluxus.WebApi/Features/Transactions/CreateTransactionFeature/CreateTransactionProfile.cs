using AutoMapper;
using Fluxus.Application.Features.Transactions.CreateTransaction;

namespace Fluxus.WebApi.Features.Transactions.CreateTransactionFeature
{
    public class CreateTransactionProfile : Profile
    {
        public CreateTransactionProfile()
        {
            CreateMap<CreateTransactionRequest, CreateTransactionCommand>();
            CreateMap<CreateTransactionResult, CreateTransactionResponse>();
        }
    }
}
