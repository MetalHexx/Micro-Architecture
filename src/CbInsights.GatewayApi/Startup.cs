﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using CbInsights.GatewayApi.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CbInsights.GatewayApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod());
            });
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Gateway API"                    
                });
            });

            var apiSettings = Configuration
                .GetSection("ApiSettings")
                .Get<ApiSettings>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddHttpClient<CustomersClient>();
            //services.AddHttpClient<OrdersClient>();
            //services.AddHttpClient<ProductsClient>();

            services.AddHttpClient();

            services.AddTransient(c => new CustomersClient(
                apiSettings.CustomersApiBaseUrl,
                c.GetRequiredService<IHttpClientFactory>().CreateClient()));

            services.AddTransient(c => new OrdersClient(
                apiSettings.OrdersApiBaseUrl,
                c.GetRequiredService<IHttpClientFactory>().CreateClient()));

            services.AddTransient(c => new ProductsClient(
                apiSettings.ProductsApiBaseUrl,
                c.GetRequiredService<IHttpClientFactory>().CreateClient()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
