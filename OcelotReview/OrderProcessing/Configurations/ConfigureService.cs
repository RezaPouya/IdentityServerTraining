namespace OrderProcessing.Configurations
{
    public static class ConfigureService
    {
        public static void ConfigureServices(this WebApplicationBuilder? builder)
        {
            if (builder is null) throw new ArgumentNullException(nameof(builder));

            builder.Services.AddControllers();

            builder.AddOcelotConfiguration();

            builder.AddAuthenticationConfiguration();
        }
    }
}