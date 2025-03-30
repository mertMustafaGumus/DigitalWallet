using DigitalWallet.Application.DTO;
using DigitalWallet.Application.Models;
using DigitalWallet.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Interfaces
{
    public interface IWalletService
    {
        /// <summary>
        /// Belirtilen kullanıcının cüzdan bakiyesini getirir.
        /// </summary>
        /// <param name="userId">Bakiyesi sorgulanacak kullanıcının ID'si</param>
        /// <returns>Kullanıcının güncel bakiye bilgisi</returns>
        Task<ServiceResponse<decimal>> GetBalanceAsync(Guid userId);
        /// <summary>
        /// Kullanıcının cüzdanına para yükler.
        /// </summary>
        /// <param name="userId">Para yüklenecek kullanıcının ID'si</param>
        /// <param name="model">Yükleme miktarı ve açıklama bilgilerini içeren model</param>
        /// <exception cref="Exception">Cüzdan bulunamadığında veya ödeme işlemi başarısız olduğunda fırlatılır</exception>
        Task<ServiceResponse<bool>> TopUpAsync(Guid userId, TopUpRequestModel model);
        /// <summary>
        /// Bir kullanıcıdan diğerine para transferi gerçekleştirir.
        /// </summary>
        /// <param name="senderUserId">Gönderici kullanıcının ID'si</param>
        /// <param name="model">Alıcı ID'si, miktar ve açıklama bilgilerini içeren model</param>
        /// <exception cref="Exception">Cüzdan bulunamadığında veya yetersiz bakiye durumunda fırlatılır</exception>,
        Task<ServiceResponse<bool>> TransferAsync(Guid senderUserId, TransferRequestModel model);
        /// <summary>
        /// Kullanıcının işlem geçmişini getirir
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına kayıt sayısı</param>
        /// <returns>İşlem geçmişi listesi</returns>
        Task<ServiceResponse<IEnumerable<TransactionHistoryDto>>> GetTransactionHistoryAsync(Guid userId, int pageNumber = 1, int pageSize = 10);
        /// <summary>
        /// Kullanıcının belirli tarih aralığındaki işlem geçmişini getirir
        /// </summary>
        /// <param name="userId">Kullanıcı kimliği</param>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="pageNumber">Sayfa numarası</param>
        /// <param name="pageSize">Sayfa başına kayıt sayısı</param>
        /// <returns>İşlem geçmişi listesi</returns>
        Task<ServiceResponse<IEnumerable<TransactionHistoryDto>>> GetTransactionHistoryByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate, int pageNumber = 1, int pageSize = 10);

    }
}
