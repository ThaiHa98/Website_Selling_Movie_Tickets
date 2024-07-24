using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SeedWork
{
    public class ApiResult<T> : ApiResultBaseClient<T>
    {
        public ApiResult() 
        {
        }

        public ApiResult(T data) : base(data) 
        {
        }

        public ApiResult(T data, bool success) : base(data, success) 
        {
        }

        public ApiResult(T data, bool success, string message, int totalCount) : base(data, success, message, totalCount)
        {
        }
    }
}
