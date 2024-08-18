namespace Customize.Contracts.Customer
{
    public class PutCustomerRequest(string id ,string name, string cellphone, string email)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Cellphone { get; set; } = cellphone;
        public string Email { get; set; } = email;
    }
}