namespace Customize.Domain.Entities
{
    public class Customer(string id, string name, string cellphone, string email, DateTime createdAt)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Cellphone { get; set; } = cellphone;
        public string Email { get; set; } = email;
        public DateTime CreatedAt { get; set; } = createdAt;
    }
}
