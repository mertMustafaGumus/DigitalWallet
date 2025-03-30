using DigitalWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Application.DTO
{
    public class TransactionHistoryDto
    {
        /// <summary>
        /// İşlem kimliği
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gönderen cüzdan kimliği (varsa)
        /// </summary>
        public Guid? SenderWalletId { get; set; }

        /// <summary>
        /// Alıcı cüzdan kimliği (varsa)
        /// </summary>
        public Guid? ReceiverWalletId { get; set; }

        /// <summary>
        /// İşlem tutarı
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// İşlem açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// İşlem tipi (TopUp veya Transfer)
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// İşlem tipi adı
        /// </summary>
        public string TypeName => Type.ToString();

        /// <summary>
        /// Bakiyeye etkisi (pozitif: giriş, negatif: çıkış)
        /// </summary>
        public decimal Impact { get; set; }
        public string? SenderName { get; set; }
        public string? ReceiverName { get; set; }
    }
}
