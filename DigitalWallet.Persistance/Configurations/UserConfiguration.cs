using DigitalWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Persistance.Configurations
{
    

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Wallet)
         .WithOne(w => w.User) 
         .HasForeignKey<Wallet>(w => w.UserId)
         .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
