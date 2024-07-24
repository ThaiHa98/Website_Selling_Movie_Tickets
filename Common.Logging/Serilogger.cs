using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.Extensions.Configuration;
using Serilog.Sinks.Elasticsearch;

namespace Common.Logging
{
    public static class Serilogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
             (context, configuration) =>
             {
                 //lấy tên ứng dụng từ môi trường, chuyển đổi thành chữ thường và thay dấy . thành dấu -
                 var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-");

                 //Lấy tên môi trường hiện tại nếu không có thì sử dụng "Development" làm giá trị mặc định
                 var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";

                 //lấy URI của Elasticsearch từ cấu hình ứng dung
                 var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");

                 //lấy tên người dùng để kết nối với Elasticsearch từ cấu hịnh ứng dụng
                 var username = context.Configuration.GetValue<string>("ElasticConfiguration:Username");

                //lấy mật khẩu đế kết nối với Elasticsearch từ cấu hình ứng dụng
                 var password = context.Configuration.GetValue<string>("ElasticConfiguration:Password");

                 if (string.IsNullOrEmpty(elasticUri))
                     throw new Exception("ElasticConfiguration Uri is not configured.");

                 configuration
                      //Thêm các thuộc tính bổ sung từ ngữ cảnh log vào các bản ghi log
                     .Enrich.FromLogContext()
                      //thêm tên máy vào các bản ghi log
                     .Enrich.WithMachineName()
                     //ghi log vào cửa sổ Bebug,hữu ích cho việc phát triển và gỡ lỗi
                     .WriteTo.Debug()
                     //Đọc cấu hình từ cấu hình ứng dụng(có thể chứa cấu hình cho các sink hoặc enrichment khác)
                     .WriteTo.Console().ReadFrom.Configuration(context.Configuration)
                     //Định dạng tên chỉ mục log trong Elasticsearch. Được cấu hình với tên ứng dụng, môi trường và tháng năm hiện tại
                     .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                     {
                         IndexFormat =
                             $"{applicationName}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                         AutoRegisterTemplate = true,
                         NumberOfShards = 2,
                         NumberOfReplicas = 1,
                         ModifyConnectionSettings = x => x.BasicAuthentication(username, password)
                     })
                     .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                     .Enrich.WithProperty("Application", applicationName)
                     .ReadFrom.Configuration(context.Configuration);
             };
    }
}
