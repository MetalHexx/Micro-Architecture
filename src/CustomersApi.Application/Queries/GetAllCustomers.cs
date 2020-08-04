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
    public class GetAllCustomers: IRequest<RepoResult<IEnumerable<Customer>>>
    {
    }

    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomers, RepoResult<IEnumerable<Customer>>>
    {
        private readonly ICustomersRepository _customersRepo;

        public GetAllCustomersHandler(ICustomersRepository customersRepo)
        {
            _customersRepo = customersRepo;
        }        
        public async Task<RepoResult<IEnumerable<Customer>>> Handle(GetAllCustomers request, CancellationToken cancellationToken)
        {
            return _customersRepo.GetCustomers();
        }
    }
}
