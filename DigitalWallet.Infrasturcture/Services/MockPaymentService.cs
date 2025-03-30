using DigitalWallet.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Infrasturcture.Services
{
    public class MockPaymentService : IPaymentService
    {
        public Task<bool> ProcessPaymentAsync(Guid userId, decimal amount)
        {
            return Task.FromResult(true);
        }
    }
}
