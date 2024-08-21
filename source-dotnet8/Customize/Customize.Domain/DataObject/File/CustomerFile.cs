using Customize.Domain.Entities;

namespace Customize.Domain.DataObject.File
{
    public class CustomerFile(string action, Customer customer)
    {
        public string Action { get; set; } = action;
        public Customer Customer { get; set; } = customer;
    }
}
