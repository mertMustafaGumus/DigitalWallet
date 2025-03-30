using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task<Wallet> GetByUserIdAsync(Guid userId);
        Task UpdateAsync(Wallet wallet);

    }

}
