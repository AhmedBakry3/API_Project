using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();

            var ObjectDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectDataSeeding.DataSeedAsync();
            await ObjectDataSeeding.IdentityDataSeedAsync();

        }
        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(Options =>
            {
                Options.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration = true
                };

                Options.JsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                Options.DocumentTitle = "My E-Commerce API";

                Options.DocExpansion(DocExpansion.None);

                Options.EnableFilter();

                Options.EnablePersistAuthorization();

            });
            return app;
        }   
    }
}
