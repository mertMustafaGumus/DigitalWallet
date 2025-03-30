using System;

namespace DigitalWallet.Domain.Entities
{
    public enum TransactionType
    {
        TopUp = 0,
        Transfer = 1
    }

    public class WalletTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Key'ler (nullable çünkü TopUp'ta biri boş olabilir)
        public Guid? SenderWalletId { get; set; }
        public Guid? ReceiverWalletId { get; set; }

        // Ana bilgiler
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public TransactionType Type { get; set; }

        // Navigation Properties
        public Wallet? SenderWallet { get; set; }
        public Wallet? ReceiverWallet { get; set; }
    }
}
