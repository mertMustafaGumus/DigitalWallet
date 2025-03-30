using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Models
{
    public class TransferRequestModel
    {
        public Guid ReceiverUserId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
