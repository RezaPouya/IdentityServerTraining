using Ocelot.Middleware;

namespace OrderProcessing.Configurations
{
    public static class ConfigureMiddleware
    {
        public static async Task ConfigureMiddlewares(this WebApplication? app)
        {
            // middleware

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            await app.UseOcelot();
        }
    }
}