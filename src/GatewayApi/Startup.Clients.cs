using GatewayApi.Clients;
using GatewayApi.Infrastructure.Clients.CustomersApi;
using GatewayApi.Infrastructure.Clients.OrdersApi;
using GatewayApi.Infrastructure.Clients.ProductsApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GatewayApi
{
    public partial class Startup
    {
        /// <summary>
        /// Configures the api clients with resilience policies.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public void ConfigureClients(IServiceCollection services)
        {
            var apiSettings = Configuration
                .GetSection("ApiSettings")
                .Get<ApiSettings>();


            services.AddHttpClient<ICustomersApiClient, CustomersApiClient>(client =>
                client.BaseAddress = new Uri(apiSettings.CustomersApiBaseUrl))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryWithBackoffPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IOrdersApiClient, OrdersApiClient>(client =>
                client.BaseAddress = new Uri(apiSettings.OrdersApiBaseUrl))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryWithBackoffPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IProductsApiClient, ProductsApiClient>(client =>
                client.BaseAddress = new Uri(apiSettings.ProductsApiBaseUrl))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryWithBackoffPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());
        }

        IAsyncPolicy<HttpResponseMessage> GetRetryWithBackoffPolicy()
        {
            Random jitterer = new Random();
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt =>
                {
                    OnHttpRetry(retryAttempt);
                    return TimeSpan.FromSeconds(1)
                                   + TimeSpan.FromMilliseconds(jitterer.Next(0, 100));
                }); ;
        }

        IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(10),
                (result, breakDuration) =>
                {
                    OnHttpBreak(result, breakDuration, 10);
                },
                () =>
                {
                    OnHttpReset();
                });
        }

        void OnHttpRetry(int retryAttempt)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Retry attempt #{retryAttempt}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void OnHttpBreak(DelegateResult<HttpResponseMessage> result, TimeSpan breakDuration, int retryCount)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Circuit open.  Service shutdown during {breakDuration} after {retryCount} failed retries.");
            Console.ForegroundColor = ConsoleColor.White;
            throw new BrokenCircuitException("Service inoperative. Please try again later");
        }

        static void OnHttpReset()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Circuit closed. Service is now accepting calls again.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
