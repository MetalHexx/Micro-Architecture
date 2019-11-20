using CbInsights.GatewayApi.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CbInsights.GatewayApi.Configuration
{
    public static class ApiClientConfig
    {
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var apiSettings = configuration
                .GetSection("ApiSettings")
                .Get<ApiSettings>();

            //services.AddHttpClient<CustomersClient>()
            //    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //    .AddPolicyHandler(GetRetryPolicy());

            //services.AddTransient(c => new CustomersClient(
            //    apiSettings.CustomersApiBaseUrl,
            //    c.GetRequiredService<IHttpClientFactory>().CreateClient()));


            services.AddTransient(c => new CustomersClient(
                apiSettings.CustomersApiBaseUrl,
                c.GetRequiredService<IHttpClientFactory>().CreateClient()));


            //services.AddHttpClient<CustomersClient, CustomersClient>(c =>
            //{
            //    new CustomersClient(
            //        apiSettings.CustomersApiBaseUrl, 
            //        c);
            //});
            //.SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //.AddPolicyHandler(GetRetryPolicy());

            //services.AddTransient(c => new CustomersClient(
            //    apiSettings.CustomersApiBaseUrl,
            //    c.GetRequiredService<IHttpClientFactory>().CreateClient()));

            services.AddHttpClient<OrdersClient>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());

            services.AddTransient(c => new OrdersClient(
                apiSettings.OrdersApiBaseUrl,
                c.GetRequiredService<IHttpClientFactory>().CreateClient()));

            services.AddHttpClient<ProductsClient>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());

            services.AddTransient(c => new ProductsClient(
                apiSettings.ProductsApiBaseUrl,
                c.GetRequiredService<IHttpClientFactory>().CreateClient()));

            //var apiSettings = configuration
            //    .GetSection("ApiSettings")
            //    .Get<ApiSettings>();

            //services.AddHttpClient();

            //services.AddHttpClient<CustomersClient>(c => new CustomersClient(apiSettings.CustomersApiBaseUrl, c))
            //    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //    .AddPolicyHandler(GetRetryPolicy());

            //services.AddHttpClient<OrdersClient>(c => new OrdersClient(apiSettings.OrdersApiBaseUrl, c))
            //    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //    .AddPolicyHandler(GetRetryPolicy());

            //services.AddHttpClient<ProductsClient>(c => new ProductsClient(apiSettings.ProductsApiBaseUrl, c))
            //    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //    .AddPolicyHandler(GetRetryPolicy());
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
