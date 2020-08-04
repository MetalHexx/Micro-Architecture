using AutoMapper;
using GatewayApi.Domain.Entities;
using GatewayApi.Infrastructure.Clients.CustomersApi;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApiCustomer = GatewayApi.Infrastructure.Clients.CustomersApi.Customer;
using Customer = GatewayApi.Domain.Entities.Customer;

namespace GatewayApi.Application.Customers.Queries
{
    public class GetAllCustomers: IRequest<IEnumerable<Customer>> { }

    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomers, IEnumerable<Customer>>
    {
        private readonly ICustomersApiClient _customersClient;
        private readonly IMapper _mapper;

        public GetAllCustomersHandler(ICustomersApiClient customersClient, IMapper mapper)
        {
            _customersClient = customersClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Customer>> Handle(GetAllCustomers request, CancellationToken cancellationToken)
        {
            var apiCustomers = await _customersClient.GetCustomersAsync(cancellationToken);
            return apiCustomers.Select(c => _mapper.Map<ApiCustomer, Customer>(c));
        }
    }
}
