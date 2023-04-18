using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace POS.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder => 
            {
                var integrationCofiguration = new ConfigurationBuilder()
                                                .AddJsonFile("appsettings.json")
                                                .AddEnvironmentVariables()
                                                .Build();

                configurationBuilder.AddConfiguration(integrationCofiguration);
            });
        }
    }
}
