namespace CustomersApi.Domain
{
    public class Customer: IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
