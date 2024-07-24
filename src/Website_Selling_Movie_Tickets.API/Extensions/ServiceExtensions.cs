﻿using Common.Logging;
using IdentityServer4.AccessTokenValidation;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Shared.Common;
using Shared.Common.Interfaces;
using Shared.Configurations;
using Shared.Exceptions;
namespace Website_Selling_Movie_Tickets.API.Extensions
{
    public static class ServiceExtensions
    {
        //dung để ánh xạ các thiết lập cấu hình từ file cấu hình appsettings.json
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration) 
        {
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>();
            services.AddSingleton(databaseSettings);

            return services;
        }

        public static void ConfigureHealthChecks(this IServiceCollection services)
        {
            // Đăng ký cấu hình
            //khi được gọi trong quá trình khởi động ứng dụng, nó sẽ đảm bảo rằng các dịch vụ cần thiết (như SQL Server) đang hoạt động đúng cách
            var databaseSettings = services.GetOptions<DatabaseSettings>(nameof(DatabaseSettings));
            services.AddHealthChecks()
                .AddSqlServer(databaseSettings.ConnectionString,
                              name: "SqlServer Health",
                              failureStatus: HealthStatus.Degraded);
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services) => 
            services.AddTransient<ISerializeService, SerializeService>()
                    .AddTransient<LoggingDelegatingHandler> ();

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Website_Selling_Movie_Tickets API V1",
                });
                c.AddSecurityDefinition(IdentityServerAuthenticationDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                { "authenticationsso_microservices_api.read", "AuthenticationSSO Microservices API Read Scope" },
                                { "authenticationsso_microservices_api.write", "AuthenticationSSO Microservices API Write Scope" }
                            }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = IdentityServerAuthenticationDefaults.AuthenticationScheme
                            },
                            Name = "Bearer"
                        },
                        new List<string>
                        {
                            "authenticationsso_microservices_api.read",
                            "authenticationsso_microservices_api.write"
                        }
                    }
                });
            });
        }

    }
}
