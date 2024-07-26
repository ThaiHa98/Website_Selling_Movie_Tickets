using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Shared.Middlewares;
using TinTuc.Application.Helper;

namespace Website_Selling_Movie_Tickets.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Website_Selling_Movie_Tickets API");
                c.DisplayRequestDuration();
            });

            app.UseMiddleware<ErrorWrappingMiddleware>();

            app.UseStaticFiles();

            app.UseAuthentication(); // Phải được khai báo trước UseAuthorization

            app.UseMiddleware<LastActivityMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
