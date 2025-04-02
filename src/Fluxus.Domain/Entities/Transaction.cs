using Fluxus.Domain.Common;
using Fluxus.Domain.Enums;

namespace Fluxus.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid UserId { get; set; }

        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }
    }
}
