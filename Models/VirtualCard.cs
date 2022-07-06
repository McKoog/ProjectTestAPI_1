using System.ComponentModel.DataAnnotations;

namespace ProjectTestAPI_1.Models
{
    public class VirtualCard
    {
        public VirtualCard(ulong cardNumber, Guid cardToken)
        {
            CardNumber = cardNumber;
            CardToken = cardToken;
        }
        [Key]
        public ulong CardNumber { get; set; }
        public Guid CardToken { get; set; }

    }
}