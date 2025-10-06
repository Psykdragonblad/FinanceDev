using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDev.Domain.Shared
{
    public class ResultResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ResultResponse<T> Ok(T data) => new() { Success = true, Data = data };
        public static ResultResponse<T> Fail(string message) => new() { Success = false, Message = message };
    }

    public class ResultResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public static ResultResponse Ok(string? message = null)
            => new () { Success = true, Message = message ?? "Operação concluída com sucesso." };

        public static ResultResponse Fail(string message)
            => new () { Success = false, Message = message };
    }
}
