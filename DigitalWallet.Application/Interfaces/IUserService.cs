using DigitalWallet.Application.DTO;
using DigitalWallet.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<List<ReceiverDto>>> GetReceiversAsync(Guid currentUserId);
    }
}
