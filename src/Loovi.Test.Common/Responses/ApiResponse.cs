using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Common.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<ValidationError>? Errors { get; set; }

        public static ApiResponse<T> Ok(T data, string? message = null)
            => new() { Success = true, Message = message ?? "Success", Data = data };

        public static ApiResponse<T> Fail(string message, List<ValidationError>? errors = null)
            => new() { Success = false, Message = message, Errors = errors };
    }
}
