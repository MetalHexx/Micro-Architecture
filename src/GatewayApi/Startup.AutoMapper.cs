using AutoMapper;
using GatewayApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayApi
{
    public partial class Startup
    {
        public void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            var config = new MapperConfiguration(cfg => {

                //Service Models
                cfg.CreateMap<Infrastructure.Clients.CustomersApi.Customer, Domain.Entities.Customer>();
                cfg.CreateMap<Infrastructure.Clients.OrdersApi.OrderItem, Domain.Entities.OrderItem>();
                cfg.CreateMap<Infrastructure.Clients.OrdersApi.Order, Domain.Entities.Order>();                
                cfg.CreateMap<Infrastructure.Clients.ProductsApi.Product, Domain.Entities.Product>();
                cfg.CreateMap<Application.CustomerOrders.Queries.CustomerOrdersWithProductsResponse, Models.CustomerOrdersViewModel>();
                cfg.CreateMap<Domain.Entities.Product, ProductViewModel>();
                cfg.CreateMap<Domain.Entities.Product, ProductViewModel>();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
        }
    }
}
