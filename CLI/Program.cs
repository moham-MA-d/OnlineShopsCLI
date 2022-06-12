using System;
using System.Text.Json;
using System.Threading.Tasks;
using CLI.Output;
using CLI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using FluentValidation;
using Polly;

namespace CLI
{
    public  class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var serviceProvider = BuildServiceProvider(configuration);
            var app = serviceProvider.GetRequiredService<OnlineShopSearchApplication>();
            await app.RunAsync(args);
        }

        //DI Container
        private static void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSingleton<IConsoleWriter, ConsoleWriter>();
            services.AddSingleton<OnlineShopSearchApplication>();
            services.AddSingleton<IOnlineShopSearchService, OnlineShopSearchService>();
            //It will automatically scan to find the validators.
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddRefitClient<IOnlineShopApi>()
                //To improve the resiliency of the API => microsoft.extensions.http.polly
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new []
                {
                    //After a transient error we wait for specified seconds bellow
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }))
                .ConfigureHttpClient(httpClient =>
                {
                    httpClient.BaseAddress = new Uri(configuration["OnlineShopApi:BaseAddress"]);
                });
        }

        private static ServiceProvider BuildServiceProvider(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration;
        }
    }
}