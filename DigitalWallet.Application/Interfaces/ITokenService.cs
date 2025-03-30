using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
