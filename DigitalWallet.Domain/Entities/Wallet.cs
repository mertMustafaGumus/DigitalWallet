using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public decimal Balance { get; set; } = 0;
        public User User { get; set; }
        //public List<WalletTransaction> SentTransactions { get; set; } = new();
        //public List<WalletTransaction> ReceivedTransactions { get; set; } = new();

        public ICollection<WalletTransaction> SentTransactions { get; set; } = new List<WalletTransaction>();
        public ICollection<WalletTransaction> ReceivedTransactions { get; set; } = new List<WalletTransaction>();
    }
}
