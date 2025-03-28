using System;

namespace Fluxus.WebApi.Features.Transactions.ListTransactionsFeature
{
    public class ListTransactionsRequest
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
