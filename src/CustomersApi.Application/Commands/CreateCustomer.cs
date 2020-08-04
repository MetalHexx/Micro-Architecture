using CustomersApi.Domain;
using CustomersApi.Infrastructure.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomersApi.Application.Commands
{
    public class CreateCustomer: IRequest<RepoResult<Customer>>
    {
        public CreateCustomer(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer { get; }
    }

    public class CreateCustomerHandler : IRequestHandler<CreateCustomer, RepoResult<Customer>>
    {
        private readonly ICustomersRepository _customersRepo;

        public CreateCustomerHandler(ICustomersRepository customersRepo)
        {
            _customersRepo = customersRepo;
        }
        public async Task<RepoResult<Customer>> Handle(CreateCustomer request, CancellationToken cancellationToken)
        {
            return _customersRepo.InsertCustomer(request.Customer);
        }
    }
}
