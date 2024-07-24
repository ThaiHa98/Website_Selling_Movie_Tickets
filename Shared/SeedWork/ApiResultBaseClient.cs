using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SeedWork
{
    public class ApiResultBaseClient<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int TatalCount { get; set; }

        public ApiResultBaseClient() 
        { 
        }
        public ApiResultBaseClient(T data) 
        {
            Data = data;
            Success = true;
        }
        public ApiResultBaseClient(T data, bool success) 
        {
            Data = data;
            Success = success;
        }
        public ApiResultBaseClient(T data, bool success, string message, int tatalCount)
        {
            Data= data;
            Success = success;
            Message = message;
            TatalCount = tatalCount;
        }
    }
}
