using Newtonsoft.Json;

namespace Shared.SeedWork
{
    public class ApiResponse
    {
        public int StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//chỉ định rằng thuộc tính sẽ bị loại bỏ khởi JSON nếu giá trị của nó là null
        public string Message { get; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 401:
                    return "You are not authenticated";

                case 404:
                    return "Resource not found";

                case 403:
                    return "You do not have permission to access this resource";

                case 500:
                    return "An unhandled error occurred";

                default:
                    return null;
            }
        }
    }
}
