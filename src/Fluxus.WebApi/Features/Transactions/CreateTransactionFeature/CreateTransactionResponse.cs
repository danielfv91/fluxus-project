﻿namespace Fluxus.WebApi.Features.Transactions.CreateTransactionFeature
{
    public class CreateTransactionResponse
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
