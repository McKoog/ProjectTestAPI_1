
using System.ComponentModel.DataAnnotations;

namespace ProjectTestAPI_1.Models
{
    public class DiscountCard
    {
        [Key]
        public ulong CardNumber { get; set; }
        public double Balance { get; set; }
    }
}