namespace Customize.Contracts.Customer
{
    public class PostCustomerRequest(string name, string cellphone, string email)
    {
        public string Name { get; set; } = name;
        public string Cellphone { get; set; } = cellphone;
        public string Email { get; set; } = email;
    }
}
