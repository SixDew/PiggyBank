using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiggyBank.Models
{
    public class Wallets : BaseModel
    {
        public required string Name { get; set; }
        [Column(TypeName = "char(3)")]
        public required string Currency { get; set; }
        [Column(TypeName = "decimal(14,2)")]
        public required decimal Balance { get; set; }
        [Column(TypeName = "timestamptz")]
        public DateTimeOffset CreatedAt { get; set; }

        [Timestamp]
        public uint Version { get; set; }
    }
}
