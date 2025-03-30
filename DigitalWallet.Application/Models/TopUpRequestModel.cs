using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Models
{
    public class TopUpRequestModel
    {
        /// <summary>
        /// Cüzdana yüklenecek miktar. Pozitif bir değer olmalıdır.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Yükleme işlemi için açıklama.
        /// </summary>
        public string Description { get; set; }
    }
}
