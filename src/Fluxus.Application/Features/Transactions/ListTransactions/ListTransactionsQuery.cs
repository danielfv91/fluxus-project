using MediatR;
using System;

namespace Fluxus.Application.Features.Transactions.ListTransactions
{
    public class ListTransactionsQuery : IRequest<List<ListTransactionsResult>>
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
