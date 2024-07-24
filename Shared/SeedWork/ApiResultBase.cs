using System.Net;

namespace Shared.SeedWork
{
    [Serializable]
    public class ApiResultBase<T> where T : class
    {
        public bool success { get; set; }
        public int httpStatusCode { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public int totalCount { get; set; }
        public ApiResultBase()
        {
            success = true;
            httpStatusCode = (int)HttpStatusCode.OK;
        }

        public ApiResultBase(ApiResultBase<T> obj)
        {
            success = obj.success;
            httpStatusCode = obj.httpStatusCode;
            message = obj.message;
            data = obj.data;
            totalCount = obj.totalCount;
        }
    }

    [Serializable]
    public class ApiResultBase : ApiResultBase<dynamic>
    {
    }
}
