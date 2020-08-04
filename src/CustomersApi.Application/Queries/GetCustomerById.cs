using CustomersApi.Domain;
using CustomersApi.Infrastructure.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomersApi.Application.Queries
{
    public class GetCustomerById: IRequest<RepoResult<Customer>> 
    {
        public GetCustomerById(int customerId)
        {
            CustomerId = customerId;
        }
        public int CustomerId { get; set; }
    }

    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerById, RepoResult<Customer>>
    {
        private readonly ICustomersRepository _customersRepo;

        public GetCustomerByIdHandler(ICustomersRepository customersRepo)
        {
            _customersRepo = customersRepo;
        }

        public async Task<RepoResult<Customer>> Handle(GetCustomerById request, CancellationToken cancellationToken)
        {
            return _customersRepo.GetCustomer(request.CustomerId);
        }
    }
}
