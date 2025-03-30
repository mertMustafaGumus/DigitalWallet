using DigitalWallet.Application.Extensions;
using DigitalWallet.Application.Interfaces;
using DigitalWallet.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Application.Models.Responses;

namespace DigitalWallet.Presentation.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public WalletController(IWalletService walletService, IHttpContextAccessor httpContextAccessor)
        {
            _walletService = walletService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Kullanıcının güncel cüzdan bakiyesini getirir.
        /// </summary>
        /// <returns>Kullanıcının bakiye bilgisi</returns>
        /// <response code="200">Bakiye başarıyla getirildi</response>
        /// <response code="401">Kullanıcı kimliği doğrulanamadı</response>
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var userId = User.GetUserId();
            var result = await _walletService.GetBalanceAsync(userId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        /// <summary>
        /// Kullanıcının cüzdanına para yükler.
        /// </summary>
        /// <param name="model">Yüklenecek miktar ve açıklama</param>
        /// <returns>İşlem sonucu</returns>
        /// <response code="200">Para yükleme işlemi başarılı</response>
        [HttpPost("topup")]
        public async Task<IActionResult> TopUp([FromBody] TopUpRequestModel model)
        {
            var userId = User.GetUserId();
            var result = await _walletService.TopUpAsync(userId, model);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Bir kullanıcıdan diğerine para transferi gerçekleştirir.
        /// </summary>
        /// <param name="model">Transfer bilgilerini içeren model</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequestModel model)
          {
            var userId = User.GetUserId();
            var result = await _walletService.TransferAsync(userId, model);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        /// <summary>
        /// Kullanıcının işlem geçmişini getirir
        /// </summary>
        /// <param name="pageNumber">Sayfa numarası (varsayılan: 1)</param>
        /// <param name="pageSize">Sayfa başına kayıt sayısı (varsayılan: 10)</param>
        /// <returns>İşlem geçmişi listesi</returns>
        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactionHistory([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userId = User.GetUserId();
            var result = await _walletService.GetTransactionHistoryAsync(userId, pageNumber, pageSize);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Kullanıcının belirli tarih aralığındaki işlem geçmişini getirir
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <param name="pageNumber">Sayfa numarası (varsayılan: 1)</param>
        /// <param name="pageSize">Sayfa başına kayıt sayısı (varsayılan: 10)</param>
        /// <returns>İşlem geçmişi listesi</returns>
        [HttpGet("transactions/date-range")]
        public async Task<IActionResult> GetTransactionHistoryByDateRange(
       [FromQuery] DateTime startDate,
       [FromQuery] DateTime endDate,
       [FromQuery] int pageNumber = 1,
       [FromQuery] int pageSize = 10)
        {
            if (startDate > endDate)
            {
                return BadRequest(ServiceResponse<object>.Failure("Başlangıç tarihi bitiş tarihinden sonra olamaz."));
            }

            var userId = User.GetUserId();
            var result = await _walletService.GetTransactionHistoryByDateRangeAsync(userId, startDate, endDate, pageNumber, pageSize);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}
