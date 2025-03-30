using DigitalWallet.Application.DTO;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Models.Responses;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.Interfaces;

namespace DigitalWallet.Infrasturcture.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IPaymentService _paymentService;
        private readonly ITransactionRepository _transactionRepository;

        public WalletService(IWalletRepository walletRepository, IPaymentService paymentService, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _paymentService = paymentService;
            _transactionRepository = transactionRepository;
        }

        public async Task<ServiceResponse<decimal>> GetBalanceAsync(Guid userId)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId);
            if (wallet == null)
                return ServiceResponse<decimal>.Failure("Cüzdan bulunamadı.");

            return ServiceResponse<decimal>.Success(wallet.Balance);
        }

        public async Task<ServiceResponse<bool>> TopUpAsync(Guid userId, TopUpRequestModel model)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId);
            if (wallet == null)
                return ServiceResponse<bool>.Failure("Cüzdan bulunamadı.");

            var isPaid = await _paymentService.ProcessPaymentAsync(userId, model.Amount);
            if (!isPaid)
                return ServiceResponse<bool>.Failure("Ödeme başarısız.");

            wallet.Balance += model.Amount;
            await _walletRepository.UpdateAsync(wallet);

            var transaction = new WalletTransaction
            {
                ReceiverWalletId = wallet.Id,
                Amount = model.Amount,
                Description = model.Description,
                Type = TransactionType.TopUp
            };

            await _transactionRepository.AddAsync(transaction);
            return ServiceResponse<bool>.Success(true);
        }

        public async Task<ServiceResponse<bool>> TransferAsync(Guid senderUserId, TransferRequestModel model)
        {
            var senderWallet = await _walletRepository.GetByUserIdAsync(senderUserId);
            var receiverWallet = await _walletRepository.GetByUserIdAsync(model.ReceiverUserId);

            if (senderWallet == null || receiverWallet == null)
                return ServiceResponse<bool>.Failure("Gönderen veya alıcı cüzdanı bulunamadı.");

            if (senderWallet.Balance < model.Amount)
                return ServiceResponse<bool>.Failure("Yetersiz bakiye.");

            senderWallet.Balance -= model.Amount;
            receiverWallet.Balance += model.Amount;

            var transaction = new WalletTransaction
            {
                SenderWalletId = senderWallet.Id,
                ReceiverWalletId = receiverWallet.Id,
                Amount = model.Amount,
                Description = model.Description,
                Type = TransactionType.Transfer
            };

            await _walletRepository.UpdateAsync(senderWallet);
            await _walletRepository.UpdateAsync(receiverWallet);
            await _transactionRepository.AddAsync(transaction);

            return ServiceResponse<bool>.Success(true);
        }

        public async Task<ServiceResponse<IEnumerable<TransactionHistoryDto>>> GetTransactionHistoryAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId);
            if (wallet == null)
                return ServiceResponse<IEnumerable<TransactionHistoryDto>>.Failure("Cüzdan bulunamadı.");

            var transactions = await _transactionRepository.GetTransactionHistoryAsync(wallet.Id, pageNumber, pageSize);

            var result = MapTransactionsToDto(transactions, wallet.Id);

            return ServiceResponse<IEnumerable<TransactionHistoryDto>>.Success(result);
        }

        public async Task<ServiceResponse<IEnumerable<TransactionHistoryDto>>> GetTransactionHistoryByDateRangeAsync(
            Guid userId,
            DateTime startDate,
            DateTime endDate,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId);
            if (wallet == null)
                return ServiceResponse<IEnumerable<TransactionHistoryDto>>.Failure("Cüzdan bulunamadı.");

            var transactions = await _transactionRepository.GetTransactionHistoryByDateRangeAsync(
                wallet.Id, startDate, endDate, pageNumber, pageSize);

            var result = MapTransactionsToDto(transactions, wallet.Id);
            return ServiceResponse<IEnumerable<TransactionHistoryDto>>.Success(result);
        }

        private IEnumerable<TransactionHistoryDto> MapTransactionsToDto(IEnumerable<WalletTransaction> transactions, Guid currentWalletId)
        {
            return transactions.Select(t => new TransactionHistoryDto
            {
                Id = t.Id,
                SenderWalletId = t.SenderWalletId,
                ReceiverWalletId = t.ReceiverWalletId,
                SenderName = t.SenderWallet?.User != null
             ? $"{t.SenderWallet.User.FirstName} {t.SenderWallet.User.LastName}"
             : null,
                ReceiverName = t.ReceiverWallet?.User != null
             ? $"{t.ReceiverWallet.User.FirstName} {t.ReceiverWallet.User.LastName}"
             : null,
                Amount = t.Amount,
                Description = t.Description,
                CreatedAt = t.CreatedAt,
                Type = t.Type,
                Impact = t.ReceiverWalletId == currentWalletId ? t.Amount : -t.Amount
            });
        }
    }
}
