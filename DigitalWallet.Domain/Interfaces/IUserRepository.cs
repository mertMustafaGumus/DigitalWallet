
using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetReceiverUsersAsync(Guid currentUserId);
    }
}
