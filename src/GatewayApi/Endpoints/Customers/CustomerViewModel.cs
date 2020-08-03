using GatewayApi.Domain.Clients.CustomersApi;

namespace GatewayApi.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CustomerViewModel(Customer customer)
        {
            Id = customer.Id.Value;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
        }
    }
}
