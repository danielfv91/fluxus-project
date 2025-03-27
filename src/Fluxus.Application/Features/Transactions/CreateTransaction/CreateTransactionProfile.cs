using AutoMapper;
using Fluxus.Domain.Entities;

namespace Fluxus.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionProfile : Profile
    {
        public CreateTransactionProfile()
        {
            CreateMap<CreateTransactionCommand, Transaction>();
            CreateMap<Transaction, CreateTransactionResult>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        }
    }
}
