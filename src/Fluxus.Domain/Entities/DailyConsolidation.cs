using Fluxus.Domain.Common;

namespace Fluxus.Domain.Entities
{
    public class DailyConsolidation : BaseEntity
    {
        public Guid UserId { get; set; }

        public DateOnly Date { get; set; }

        public decimal TotalCredits { get; set; }

        public decimal TotalDebits { get; set; }

        public decimal Balance => TotalCredits - TotalDebits;
    }
}
