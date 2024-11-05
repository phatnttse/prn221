using System;

namespace BusinessObjects.Models
{
    public class AppResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } 
        public T Data { get; set; } 
        public string ErrorCode { get; set; } 

        public AppResponse() { }

        public AppResponse(bool success, string message, T data = default, string errorCode = null)
        {
            Success = success;
            Message = message;
            Data = data;
            ErrorCode = errorCode;
        }
    }
}
