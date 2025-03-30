using DigitalWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Persistance.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasOne(w => w.User)
                   .WithOne(u => u.Wallet)
                   .HasForeignKey<Wallet>(w => w.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // Optional, cascade istemiyorsan

            builder.HasMany(w => w.SentTransactions)
                   .WithOne()
                   .HasForeignKey(t => t.SenderWalletId)
                   .OnDelete(DeleteBehavior.Restrict);


            //builder.HasMany(w => w.ReceivedTransactions)
            //       .WithOne()
            //       .HasForeignKey(t => t.ReceiverWalletId)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
