using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Application.Models.Responses
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public Dictionary<string, string[]>? ErrorMessages { get; set; }

        public static ServiceResponse<T> Success(T? data)
        {
            return new ServiceResponse<T> { IsSuccess = true, Data = data };
        }

        public static ServiceResponse<T> Failure(string message)
        {
            return new ServiceResponse<T>
            {
                IsSuccess = false,
                ErrorMessages = new Dictionary<string, string[]> { { "errors", new[] { message } } }
            };
        }

        public static ServiceResponse<T> Failure(Dictionary<string, string[]> errors)
        {
            return new ServiceResponse<T>
            {
                IsSuccess = false,
                ErrorMessages = errors
            };
        }
    }

}
