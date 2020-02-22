using GatewayApi.Clients;
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

namespace GatewayApi.Configuration
{
    public static class ApiClientConfig
    {
        /// <summary>
        /// Configures the api clients with resilience policies.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var apiSettings = configuration
                .GetSection("ApiSettings")
                .Get<ApiSettings>();

            services.AddHttpClient<CustomersClient>(client => 
                client.BaseAddress = new Uri(apiSettings.CustomersApiBaseUrl))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryWithBackoffPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<OrdersClient>(client =>
                client.BaseAddress = new Uri(apiSettings.OrdersApiBaseUrl))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryWithBackoffPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<ProductsClient>(client =>
                client.BaseAddress = new Uri(apiSettings.ProductsApiBaseUrl))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryWithBackoffPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryWithBackoffPolicy()
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
                });;
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
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

        static void OnHttpRetry(int retryAttempt)
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
