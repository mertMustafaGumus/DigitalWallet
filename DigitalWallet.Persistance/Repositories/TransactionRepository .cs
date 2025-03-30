using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Interfaces;
using DigitalWallet.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Persistance.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WalletTransaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WalletTransaction>> GetTransactionHistoryAsync(Guid walletId, int pageNumber, int pageSize)
        {
            return await _context.Transactions
                .Include(t => t.SenderWallet).ThenInclude(w => w.User)
                .Include(t => t.ReceiverWallet).ThenInclude(w => w.User)
                .Where(t => t.SenderWalletId == walletId || t.ReceiverWalletId == walletId)
                .OrderByDescending(t => t.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<WalletTransaction>> GetTransactionHistoryByDateRangeAsync(
          Guid walletId,
          DateTime startDate,
          DateTime endDate,
          int pageNumber = 1,
          int pageSize = 10)
        {
            return await _context.Transactions
                .Where(t => (t.SenderWalletId == walletId || t.ReceiverWalletId == walletId) &&
                           t.CreatedAt >= startDate && t.CreatedAt <= endDate)
                .OrderByDescending(t => t.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(t => t.SenderWallet)
                .Include(t => t.ReceiverWallet)
                .ToListAsync();
        }
    }

}
