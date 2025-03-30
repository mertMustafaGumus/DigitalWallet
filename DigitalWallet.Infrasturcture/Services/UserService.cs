using DigitalWallet.Application.DTO;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Application.Models.Responses;
using DigitalWallet.Domain.Interfaces;

namespace DigitalWallet.Infrasturcture.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse<List<ReceiverDto>>> GetReceiversAsync(Guid currentUserId)
        {
            var users = await _userRepository.GetReceiverUsersAsync(currentUserId);

            var dto = users.Select(u => new ReceiverDto
            {
                UserId = u.Id,
                FullName = $"{u.FirstName} {u.LastName}",
                Email = u.Email
            }).ToList();
            return ServiceResponse<List<ReceiverDto>>.Success(dto);
        }
    }
}
