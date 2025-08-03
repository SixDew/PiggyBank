using PiggyBank.Resources;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiggyBank.Models
{
    public class Transactions : BaseModel
    {
        public required Guid WalletId { get; set; }
        [Column(TypeName = "decimal(14,2)")]
        public required decimal Amount { get; set; }
        [Column(TypeName = "smallint")]
        public required TransactionDirection Direction { get; set; }
        [Column(TypeName = "timestamptz")]
        public required DateTimeOffset OccurredAt { get; set; }
        public string? Comment { get; set; }
        [Column(TypeName = "timestamptz")]
        public DateTimeOffset CreatedAt { get; set; }
    }

}
