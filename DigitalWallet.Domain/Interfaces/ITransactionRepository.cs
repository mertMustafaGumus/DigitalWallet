using DigitalWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Domain.Interfaces
{
    /// <summary>
    /// İşlem kayıtlarıyla ilgili veritabanı operasyonlarını yönetir
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Yeni bir işlem kaydı ekler
        /// </summary>
        /// <param name="transaction">Eklenecek işlem</param>
        Task AddAsync(WalletTransaction transaction);

        /// <summary>
        /// Kullanıcının cüzdanına ait işlem geçmişini getirir
        /// </summary>
        /// <param name="walletId">Cüzdan kimliği</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına kayıt sayısı</param>
        /// <returns>İşlem listesi</returns>
        Task<IEnumerable<WalletTransaction>> GetTransactionHistoryAsync(Guid walletId, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Kullanıcının belirli bir zaman aralığındaki işlem geçmişini getirir
        /// </summary>
        /// <param name="walletId">Cüzdan kimliği</param>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına kayıt sayısı</param>
        /// <returns>İşlem listesi</returns>
        Task<IEnumerable<WalletTransaction>> GetTransactionHistoryByDateRangeAsync(Guid walletId, DateTime startDate, DateTime endDate, int pageNumber = 1, int pageSize = 10);
    }

}
